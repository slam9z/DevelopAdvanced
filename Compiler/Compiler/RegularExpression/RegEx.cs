using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace RegularExpression
{

    /// <summary>
    /// the regular expression recognizer class
    /// </summary>
    public class RegEx
    {
        /// <summary>
        /// Used to validate the pattern string and make sure it is in correct form
        /// </summary>
        static private RegExValidator m_reValidator = new RegExValidator();

        /// <summary>
        /// Position of the last error in the pattern string.
        /// 
        /// </summary>
        private int m_nLastErrorIndex = -1;

        /// <summary>
        /// Length of the error substring in the pattern string
        /// </summary>
        private int m_nLastErrorLength = -1;

        /// <summary>
        /// ErrorCode indicating what if the last compilation succeded.
        /// </summary>
        private ErrorCode m_LastErrorCode = ErrorCode.ERR_SUCCESS;

        /// <summary>
        /// Specifies match must occure at the begining of the input string (^)
        /// </summary>
        private bool m_bMatchAtStart = false;

        /// <summary>
        ///  Specifies match must occure at the end of the input string ($)
        /// </summary>
        private bool m_bMatchAtEnd = false;

        /// <summary>
        /// Behave greedy, default is true.
        /// If true, Find will stop at the first accepting state, otherwisie it will try process more charectr
        /// </summary>
        private bool m_bGreedy = true;

        /// <summary>
        /// Start state of the DFA M'.
        /// </summary>
        private State m_stateStartDfaM = null;


        /// <summary>
        /// public default constructor
        /// </summary>
        public RegEx()
        {

        }


        /// <summary>
        /// to keep set of distinct input symbol. needed for NFA to DFA conversion
        /// </summary>
        //private Set m_setInputSymbol = new Set();

        /// <summary>
        /// Converts a regular expression from infix form to postfix form
        /// </summary>
        /// <param name="sInfixPattern">regular expression in infix form</param>
        /// <returns>regular expression in postfix form</returns>
        private string ConvertToPostfix(string sInfixPattern)
        {
            Stack stackOperator = new Stack();
            Queue queuePostfix = new Queue();
            bool bEscape = false;

            for (int i = 0; i < sInfixPattern.Length; i++)
            {
                char ch = sInfixPattern[i];

                //处理转意字符。

                if (bEscape == false && ch == MetaSymbol.ESCAPE)
                {
                    queuePostfix.Enqueue(ch);
                    bEscape = true;
                    continue;
                }

                if (bEscape == true)
                {
                    queuePostfix.Enqueue(ch);
                    bEscape = false;
                    continue;
                }
                switch (ch)
                {
                    //()只是确定优先级所以没有后缀表示法不包含它们

                    case MetaSymbol.OPEN_PREN:
                        stackOperator.Push(ch);
                        break;
                    case MetaSymbol.CLOSE_PREN:
                        while ((char)stackOperator.Peek() != MetaSymbol.OPEN_PREN)
                        {
                            queuePostfix.Enqueue(stackOperator.Pop());
                        }
                        stackOperator.Pop();  // pop the '('

                        break;
                    default:
                        //因为一直有一个. ,所以后面的就直接插入了。
                        while (stackOperator.Count > 0)
                        {
                            char chPeeked = (char)stackOperator.Peek();

                            int nPriorityPeek = GetOperatorPriority(chPeeked);
                            int nPriorityCurr = GetOperatorPriority(ch);

                            //如果前一个符号优先级别高就入列。

                            if (nPriorityPeek >= nPriorityCurr)
                            {
                                queuePostfix.Enqueue(stackOperator.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                        stackOperator.Push(ch);
                        break;
                }

            }  // end of for..loop

            while (stackOperator.Count > 0)
            {
                queuePostfix.Enqueue((char)stackOperator.Pop());
            }
            StringBuilder sb = new StringBuilder(1024);
            while (queuePostfix.Count > 0)
            {
                sb.Append((char)queuePostfix.Dequeue());
            }


            return sb.ToString();
        }

        /// <summary>
        /// helper function. needed for postfix conversion
        /// </summary>
        /// <param name="chOpt">literal symbol</param>
        /// <returns>priority</returns>
        private int GetOperatorPriority(char chOpt)
        {
            switch (chOpt)
            {
                case MetaSymbol.OPEN_PREN:
                    return 0;
                case MetaSymbol.ALTERNATE:
                    return 1;
                case MetaSymbol.CONCANATE:
                    return 2;
                case MetaSymbol.ZERO_OR_ONE:
                case MetaSymbol.ZERO_OR_MORE:
                case MetaSymbol.ONE_OR_MORE:
                    return 3;
                case MetaSymbol.COMPLEMENT:
                    return 4;
                default:
                    return 5;

            }
        }


        /// <summary>
        /// Compiles a pattern string produces a Minimum DFA model.
        /// </summary>
        /// <param name="sPattern">Actual pattern string in the correct format</param>
        /// <param name="sbStats">This will receive the statistics. Can be null.</param>
        /// <returns>ErrorCode indicating how the compilatio went.</returns>
        public ErrorCode CompileWithStats(string sPattern, StringBuilder sbStats)
        {

            if (sbStats == null)
            {
                return Compile(sPattern);  // no statistics required
            }

            State.ResetCounter();

            int nLineLength = 0;

            ValidationInfo vi = m_reValidator.Validate(sPattern);

            UpdateValidationInfo(vi);

            if (vi.ErrorCode != ErrorCode.ERR_SUCCESS)
            {
                return vi.ErrorCode;
            }

            //虽然注意到了connection问题,但是没想到用.号表示。

            string sRegExPostfix = ConvertToPostfix(vi.FormattedString);

            sbStats.AppendLine("Original pattern:\t\t" + sPattern);
            sbStats.AppendLine("Pattern after formatting:\t" + vi.FormattedString);
            sbStats.AppendLine("Pattern after postfix:\t\t" + sRegExPostfix);
            sbStats.AppendLine();

            State stateStartNfa = CreateNfa(sRegExPostfix);
            sbStats.AppendLine();
            sbStats.AppendLine("NFA Table:");
            nLineLength = GetSerializedFsa(stateStartNfa, sbStats);
            sbStats.AppendFormat(("").PadRight(nLineLength, '*'));
            sbStats.AppendLine();

            State.ResetCounter();
            State stateStartDfa = ConvertToDfa(stateStartNfa);
            sbStats.AppendLine();
            sbStats.AppendLine("DFA Table:");
            nLineLength = GetSerializedFsa(stateStartDfa, sbStats);
            sbStats.AppendFormat(("").PadRight(nLineLength, '*'));
            sbStats.AppendLine();

            State stateStartDfaM = ReduceDfa(stateStartDfa);
            m_stateStartDfaM = stateStartDfaM;
            sbStats.AppendLine();
            sbStats.AppendLine("DFA M' Table:");
            nLineLength = GetSerializedFsa(stateStartDfaM, sbStats);
            sbStats.AppendFormat(("").PadRight(nLineLength, '*'));
            sbStats.AppendLine();

            return ErrorCode.ERR_SUCCESS;


        }

        /// <summary>
        /// Compiles a pattern string produces a Minimum DFA model.
        /// </summary>
        /// <param name="sPattern">Actual pattern string in the correct format</param>
        /// <returns>ErrorCode indicating how the compilatio went.</returns>
        public ErrorCode Compile(string sPattern)
        {
            ValidationInfo vi = m_reValidator.Validate(sPattern);

            UpdateValidationInfo(vi);

            if (vi.ErrorCode != ErrorCode.ERR_SUCCESS)
            {
                return vi.ErrorCode;
            }


            State.ResetCounter();
            string sRegExConcat = vi.FormattedString;

            string sRegExPostfix = ConvertToPostfix(sRegExConcat);

            State stateStartNfa = CreateNfa(sRegExPostfix);

            State.ResetCounter();
            State stateStartDfa = ConvertToDfa(stateStartNfa);
            m_stateStartDfaM = stateStartDfa;

            m_stateStartDfaM = ReduceDfa(stateStartDfa);

            return ErrorCode.ERR_SUCCESS;

        }

        /// <summary>
        /// Finds all state reachable from the specic state on Epsilon transition
        /// </summary>
        /// <param name="stateStart">State from which search begins</param>
        /// <returns>A set of all state reachable from teh startState on Epsilon transtion</returns>
        private Set Eclosure(State stateStart)
        {
            Set setProcessed = new Set();
            Set setUnprocessed = new Set();

            setUnprocessed.AddElement(stateStart);

            while (setUnprocessed.Count > 0)
            {
                State state = (State)setUnprocessed[0];
                State[] arrTrans = state.GetTransitions(MetaSymbol.EPSILON);
                setProcessed.AddElement(state);
                setUnprocessed.RemoveElement(state);

                if (arrTrans != null)
                {
                    foreach (State stateEpsilon in arrTrans)
                    {
                        if (!setProcessed.ElementExist(stateEpsilon))
                        {
                            setUnprocessed.AddElement(stateEpsilon);
                        }
                    }
                }


            }

            return setProcessed;

        }

        /// <summary>
        /// Finds all state reachable from the set of states on Epsilon transition
        /// </summary>
        /// <param name="setState">Set of states to search from</param>
        /// <returns></returns>
        private Set Eclosure(Set setState)
        {
            Set setAllEclosure = new Set();
            State state = null;
            foreach (object obj in setState)
            {
                state = (State)obj;

                Set setEclosure = Eclosure(state);
                setAllEclosure.Union(setEclosure);
            }
            return setAllEclosure;
        }

        /// <summary>
        /// Gets Move of a set states.
        /// </summary>
        /// <param name="setState">Set of state for which to get Move </param>
        /// <param name="chInputSymbol">Innput symbol</param>
        /// <returns>Set of Move</returns>
        private Set Move(Set setState, string sInputSymbol)
        {
            Set set = new Set();
            State state = null;
            foreach (object obj in setState)
            {
                state = (State)obj;
                Set setMove = Move(state, sInputSymbol);
                set.Union(setMove);
            }
            return set;
        }

        /// <summary>
        /// Gets Move of a state.
        /// </summary>
        /// <param name="state">state for which to get Move</param>
        /// <param name="chInputSymbol">Input symbol</param>
        /// <returns>Set of Move</returns>
        private Set Move(State state, string sInputSymbol)
        {
            Set set = new Set();

            State[] arrTrans = state.GetTransitions(sInputSymbol);

            if (arrTrans != null)
            {
                set.AddElementRange(arrTrans);
            }

            return set;

        }

        //通过后缀表达式直接创建NFA，我还打算用树

        /// <summary>
        /// Converts a regular expression in posfix form to NFA using "Thompson抯 Construction"
        /// </summary>
        /// <param name="sRegExPosfix">Regulare expression in postfix form (pattern)</param>
        /// <returns>Start state of the NFA</returns>
        private State CreateNfa(string sRegExPosfix)
        {
            Stack stackNfa = new Stack();

            //就是一个树形结构吧！
            NfaExpression expr = null;
            NfaExpression exprA = null;
            NfaExpression exprB = null;
            NfaExpression exprNew = null;
            bool bEscape = false;

            foreach (char ch in sRegExPosfix)
            {
                if (bEscape == false && ch == MetaSymbol.ESCAPE)
                {
                    bEscape = true;
                    continue;
                }

                if (bEscape == true)
                {
                    exprNew = new NfaExpression();
                    exprNew.StartState().AddTransition(ch.ToString(), exprNew.FinalState());

                    stackNfa.Push(exprNew);

                    bEscape = false;
                    continue;
                }

                //McMaughton-Yamada-Thompson 转换算法
                switch (ch)
                {
                    case MetaSymbol.ZERO_OR_MORE:  // A*  Kleene closure

                        exprA = (NfaExpression)stackNfa.Pop();
                        exprNew = new NfaExpression();

                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());

                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());

                        // exprNew的StartState可以通过EPSILON进入自己的FinalState
                        // 也可以进入exprA的StartState
                        // 而exprA的FinalState可以进入自己的StartState或者exprNew的FinalState
                        // 因为exprA本来可以通过输入'A'进入从StartState进入FinalState
                        // 所以这个exprNew可以识别A*。

                        stackNfa.Push(exprNew);

                        break;
                    case MetaSymbol.ALTERNATE:  // A|B

                        exprB = (NfaExpression)stackNfa.Pop();
                        exprA = (NfaExpression)stackNfa.Pop();

                        exprNew = new NfaExpression();

                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());
                        exprB.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());

                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprB.StartState());

                        // StartState进入A与B的StartState，A与B的FinalState进入exprNew的FinalState。
                        stackNfa.Push(exprNew);

                        break;

                    case MetaSymbol.CONCANATE:  // AB

                        //CONCANATE比较简单。有exprA的StartState和exprB的FinalState构成新的State
                        //之间用EPSILON转换

                        exprB = (NfaExpression)stackNfa.Pop();
                        exprA = (NfaExpression)stackNfa.Pop();

                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprB.StartState());

                        exprNew = new NfaExpression(exprA.StartState(), exprB.FinalState());
                        stackNfa.Push(exprNew);

                        break;

                        //剩下几个书上没讲不是很理解。
                    case MetaSymbol.ONE_OR_MORE:  // A+ => AA* => A.A*

                        exprA = (NfaExpression)stackNfa.Pop();
                        exprNew = new NfaExpression();

                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprNew.FinalState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());

                        stackNfa.Push(exprNew);

                        break;
                    case MetaSymbol.ZERO_OR_ONE:  // A? => A|empty  
                        exprA = (NfaExpression)stackNfa.Pop();
                        exprNew = new NfaExpression();

                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());
                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());

                        stackNfa.Push(exprNew);

                        break;
                    case MetaSymbol.ANY_ONE_CHAR:
                        exprNew = new NfaExpression();
                        exprNew.StartState().AddTransition(MetaSymbol.ANY_ONE_CHAR_TRANS, exprNew.FinalState());
                        stackNfa.Push(exprNew);
                        break;

                    case MetaSymbol.COMPLEMENT:  // ^ 

                        exprA = (NfaExpression)stackNfa.Pop();

                        NfaExpression exprDummy = new NfaExpression();
                        exprDummy.StartState().AddTransition(MetaSymbol.DUMMY, exprDummy.FinalState());

                        exprA.FinalState().AddTransition(MetaSymbol.EPSILON, exprDummy.StartState());

                        NfaExpression exprAny = new NfaExpression();
                        exprAny.StartState().AddTransition(MetaSymbol.ANY_ONE_CHAR_TRANS, exprAny.FinalState());


                        exprNew = new NfaExpression();
                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprA.StartState());
                        exprNew.StartState().AddTransition(MetaSymbol.EPSILON, exprAny.StartState());

                        exprAny.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());
                        exprDummy.FinalState().AddTransition(MetaSymbol.EPSILON, exprNew.FinalState());

                        stackNfa.Push(exprNew);

                        break;
                    default:

                        //普通字符就是表达式，两个状态，包含自身符号的转移。

                        exprNew = new NfaExpression();
                        exprNew.StartState().AddTransition(ch.ToString(), exprNew.FinalState());

                        stackNfa.Push(exprNew);

                        break;

                } // end of switch statement


            }  // end of for..each loop

            Debug.Assert(stackNfa.Count == 1);
            expr = (NfaExpression)stackNfa.Pop();  // pop the very last one.  YES, THERE SHOULD ONLY BE ONE LEFT AT THIS POINT
            expr.FinalState().AcceptingState = true;  // the very last state is the accepting state of the NFA

            return expr.StartState();  // retun the start state of NFA

        }  // end of CreateNfa method

        /// <summary>
        /// Converts NFA to DFA using "Subset Construction"
        /// </summary>
        /// <param name="stateStartNfa">Starting state of NFA</param>
        /// <param name="setMasterDfa">Contains set of all DFA states when this function returns</param>
        /// <returns>Starting state of DFA</returns>
        private State ConvertToDfa(State stateStartNfa)
        {
            Set setAllInput = new Set();
            Set setAllState = new Set();

            GetAllStateAndInput(stateStartNfa, setAllState, setAllInput);
            setAllInput.RemoveElement(MetaSymbol.EPSILON);

            NfaToDfaHelper helper = new NfaToDfaHelper();
            Set setMove = null;
            Set setEclosure = null;

            // first, we get Eclosure of the start state of NFA ( just following the algoritham)
            setEclosure = Eclosure(stateStartNfa);
            State stateStartDfa = new State();  // create a new DFA state to represent the above Eclosure

            // NOTE: 
            // we keep track of the NFA Eclosure and the DFA state that represent the Eclosure.
            // we maintain a relationship between the NFA Eclosure and DFA state that represents the NFA Eclosure.
            // all these are done in the NfaToDfaHelper class.

            if (IsAcceptingGroup(setEclosure) == true)
            {
                stateStartDfa.AcceptingState = true;
            }

            helper.AddDfaState(stateStartDfa, setEclosure);

            string sInputSymbol = String.Empty; // dummy

            // please see "subset construction" algoritham
            // for clear understanding

            State stateT = null;
            Set setT = null;
            State stateU = null;

            while ((stateT = helper.GetNextUnmarkedDfaState()) != null)
            {
                helper.Mark(stateT);   // flag it to indicate that we have processed this state.

                // the DFA state stateT represents a set of NFA Eclosure.
                // so, we retrieve the Eclosure.
                setT = helper.GetEclosureByDfaState(stateT);

                foreach (object obj in setAllInput)
                {
                    sInputSymbol = obj.ToString();

                    setMove = Move(setT, sInputSymbol);

                    if (setMove.IsEmpty() == false)
                    {
                        setEclosure = Eclosure(setMove);

                        stateU = helper.FindDfaStateByEclosure(setEclosure);

                        if (stateU == null) // so set setEclosure must be a new one and we should crate a new DFA state
                        {
                            stateU = new State();
                            if (IsAcceptingGroup(setEclosure) == true)
                            {
                                stateU.AcceptingState = true;
                            }

                            helper.AddDfaState(stateU, setEclosure);  // add new state (as unmarked by default)
                        }

                        stateT.AddTransition(sInputSymbol, stateU);
                    }

                }  // end of foreach..loop

            }  // end of while..loop

            return stateStartDfa;

        }  // end of ConverToDfa method

        /// <summary>
        /// Converts DFA to Minimum DFA or DFA M'.
        /// </summary>
        /// <param name="stateStartDfa">Starting state of DFA</param>
        /// <param name="setMasterDfa">Set of all DFA state (including the starting one)</param>
        /// <param name="setInputSymbol">Set of all input symbol</param>
        /// <returns>Starting state of DFA M'</returns>
        private State ReduceDfa(State stateStartDfa)
        {
            Set setInputSymbol = new Set();
            Set setAllDfaState = new Set();

            GetAllStateAndInput(stateStartDfa, setAllDfaState, setInputSymbol);


            State stateStartReducedDfa = null;   // start state of the Reduced DFA
            ArrayList arrGroup = null;  // master array of all possible partitions/groups

            // STEP 1: partition the DFS states.
            // we do this by calling another method 
            arrGroup = PartitionDfaGroups(setAllDfaState, setInputSymbol);

            // NOTE: arrGroup now contains all possible groups for all the DFA state.


            // STEP 2: now we go through all the groups and select a group representative for each group.
            // eventually the representive becomes one of the state in DFA M'.  All other members of the groups get eliminited (deleted).
            foreach (object objGroup in arrGroup)
            {
                Set setGroup = (Set)objGroup;

                bool bAcceptingGroup = IsAcceptingGroup(setGroup);  // see if the group contains any accepting state
                bool bStartingGroup = setGroup.ElementExist(stateStartDfa); // check if the group contains the starting DFA state

                // choose group representative
                State stateRepresentative = (State)setGroup[0]; // just choose the first one as group representative


                // should the representative be start state of DFA M'
                if (bStartingGroup == true)
                {
                    stateStartReducedDfa = stateRepresentative;
                }

                // should the representative be an accepting state of DFA M'
                if (bAcceptingGroup == true)
                {
                    stateRepresentative.AcceptingState = true;
                }

                if (setGroup.GetCardinality() == 1)
                {
                    continue;  // no need for further processing
                }


                // STEP 3: remove the representative from its group
                // and replace all the references of the remaining member of the group with the representative
                setGroup.RemoveElement(stateRepresentative);

                State stateToBeReplaced = null;   // state to be replaced with the group representative
                int nReplecementCount = 0;
                foreach (object objStateToReplaced in setGroup)
                {
                    stateToBeReplaced = (State)objStateToReplaced;

                    setAllDfaState.RemoveElement(stateToBeReplaced);  // remove this member from the master set as well

                    foreach (object objState in setAllDfaState)
                    {
                        State state = (State)objState;
                        nReplecementCount += state.ReplaceTransitionState(stateToBeReplaced, stateRepresentative);
                    }

                    // here, in C++, you would actully delete the stateA object by calling:
                    // delete stateToBeRemoved;

                }
            }  // end of outer foreach..loop

            //  STEP 4: now remove all "dead states"
            int nIndex = 0;
            while (nIndex < setAllDfaState.Count)
            {
                State state = (State)setAllDfaState[nIndex];
                if (state.IsDeadState())
                {
                    setAllDfaState.RemoveAt(nIndex);
                    // here, in C++, you would actully delete the stateDead object by calling:
                    // delete stateDead;
                    continue;
                }
                nIndex++;
            }


            return stateStartReducedDfa;
        }

        /// <summary>
        /// Helper function. Check to see if a set contains any accpeting state.
        /// </summary>
        /// <param name="setGroup">Set of state</param>
        /// <returns>true if set contains any accpting states, otherwise false</returns>
        private bool IsAcceptingGroup(Set setGroup)
        {
            State state = null;

            foreach (object objState in setGroup)
            {
                state = (State)objState;

                if (state.AcceptingState == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Partions set of all DFA states into smaller groups (according to the partion rules).
        /// Please see notes for detail of partioning DFA.
        /// </summary>
        /// <param name="setMasterDfa">Set of all DFA states</param>
        /// <param name="setInputSymbol">Set of all input symbol</param>
        /// <returns>Array of DFA groups</returns>
        private ArrayList PartitionDfaGroups(Set setMasterDfa, Set setInputSymbol)
        {
            ArrayList arrGroup = new ArrayList();  // array of all set (group) of DFA states.
            Map map = new Map();   // to keep track of which memeber transition into which group
            Set setEmpty = new Set();

            // first we need to create two partition of the setMasterDfa:
            // one with all the accepting states and the other one with all the non-accpeting states
            Set setAccepting = new Set();  // group of all accepting state
            Set setNonAccepting = new Set();  // group of all non-accepting state

            foreach (object objState in setMasterDfa)
            {
                State state = (State)objState;

                if (state.AcceptingState == true)
                {
                    setAccepting.AddElement(state);
                }
                else
                {
                    setNonAccepting.AddElement(state);
                }
            }

            if (setNonAccepting.GetCardinality() > 0)
            {
                arrGroup.Add(setNonAccepting);  // add this newly created partition to the master list
            }

            // for accepting state, there should always be at least one state, if NOT then there must be something wrong somewhere
            arrGroup.Add(setAccepting);   // add this newly created partition to the master list


            // now we iterate through these two partitions and see if they can be further partioned.
            // we continuew the iteration until no further paritioning is possible.

            IEnumerator iterInput = setInputSymbol.GetEnumerator();

            iterInput.Reset();

            while (iterInput.MoveNext())
            {
                string sInputSymbol = iterInput.Current.ToString();

                int nPartionIndex = 0;
                while (nPartionIndex < arrGroup.Count)
                {
                    Set setToBePartitioned = (Set)arrGroup[nPartionIndex];
                    nPartionIndex++;

                    if (setToBePartitioned.IsEmpty() || setToBePartitioned.GetCardinality() == 1)
                    {
                        continue;   // because we can't partition a set with zero or one memeber in it
                    }

                    foreach (object objState in setToBePartitioned)
                    {
                        State state = (State)objState;
                        State[] arrState = state.GetTransitions(sInputSymbol.ToString());

                        if (arrState != null)
                        {
                            Debug.Assert(arrState.Length == 1);

                            State stateTransionTo = arrState[0];  // since the state is DFA state, this array should contain only ONE state

                            Set setFound = FindGroup(arrGroup, stateTransionTo);
                            map.Add(setFound, state);
                        }
                        else   // no transition exists, so transition to empty set
                        {
                            //setEmpty = new Set();
                            map.Add(setEmpty, state);  // keep a map of which states transtion into which group

                        }
                    }  // end of foreach (object objState in setToBePartitioned)

                    if (map.Count > 1)  // means some states transition into different groups
                    {
                        arrGroup.Remove(setToBePartitioned);
                        foreach (DictionaryEntry de in map)
                        {
                            Set setValue = (Set)de.Value;
                            arrGroup.Add(setValue);
                        }
                        nPartionIndex = 0;  // we want to start from the begining again
                        iterInput.Reset();  // we want to start from the begining again
                    }
                    map.Clear();
                }  // end of while..loop


            }  // end of foreach (object objString in setInputSymbol)

            return arrGroup;
        }  // end of PartitionDfaSet method

        /// <summary>
        /// Helper function.  Finds a set in a array of set for a particular state.
        /// </summary>
        /// <param name="arrGroup">Array of set of states</param>
        /// <param name="state">State to search for</param>
        /// <returns>Set the state belongs to</returns>
        private Set FindGroup(ArrayList arrGroup, State state)
        {
            foreach (object objSet in arrGroup)
            {
                Set set = (Set)objSet;

                if (set.ElementExist(state) == true)
                {
                    return set;
                }
            }

            return null;
        }

        /// <summary>
        /// Retuns a string format of NFA.Set type.
        /// Only used for debugging.
        /// </summary>
        /// <param name="set">Set of state.</param>
        /// <returns>Formatted string</returns>
        private string SetToString(Set set)
        {
            string s = "";
            foreach (object objState in set)
            {
                State state = (State)objState;
                s += state.Id.ToString() + ", ";
            }

            s = s.TrimEnd(new char[] { ' ', ',' });
            if (s.Length == 0)
            {
                s = "Empty";
            }
            s = "{" + s + "}";
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateStart">start state of the model</param>
        /// <param name="setProcessed">when function returns, this set contains all the states</param>
        /// <param name="setAllState">when function returns, this set contains all the input sybmols</param>
        static internal void GetAllStateAndInput(State stateStart, Set setAllState, Set setInputSymbols)
        {
            Set setUnprocessed = new Set();

            setUnprocessed.AddElement(stateStart);

            while (setUnprocessed.Count > 0)
            {
                State state = (State)setUnprocessed[0];

                setAllState.AddElement(state);
                setUnprocessed.RemoveElement(state);

                foreach (object objToken in state.GetAllKeys())
                {
                    string sSymbol = (string)objToken;
                    setInputSymbols.AddElement(sSymbol);

                    State[] arrTrans = state.GetTransitions(sSymbol);

                    if (arrTrans != null)
                    {
                        foreach (State stateEpsilon in arrTrans)
                        {
                            if (!setAllState.ElementExist(stateEpsilon))
                            {
                                setUnprocessed.AddElement(stateEpsilon);
                            }
                        }  // end of inner foreach..loop
                    }

                }  // end of outer foreach..loop

            }  // end of outer while..loop      

        }
        static internal int GetSerializedFsa(State stateStart, StringBuilder sb)
        {
            Set setAllState = new Set();
            Set setAllInput = new Set();
            GetAllStateAndInput(stateStart, setAllState, setAllInput);
            return GetSerializedFsa(stateStart, setAllState, setAllInput, sb);
        }
        static internal int GetSerializedFsa(State stateStart, Set setAllState, Set setAllSymbols, StringBuilder sb)
        {
            int nLineLength = 0;
            int nMinWidth = 6;
            string sLine = String.Empty;
            string sFormat = String.Empty;
            setAllSymbols.RemoveElement(MetaSymbol.EPSILON);
            setAllSymbols.AddElement(MetaSymbol.EPSILON); // adds it at the end;

            // construct header row and format string
            object[] arrObj = new object[setAllSymbols.Count + 1];// the extra one becuase of the first State column
            arrObj[0] = "State";
            sFormat = "{0,-8}";
            for (int i = 0; i < setAllSymbols.Count; i++)
            {
                string sSymbol = setAllSymbols[i].ToString();
                arrObj[i + 1] = sSymbol;

                sFormat += " | ";
                sFormat += "{" + (i + 1).ToString() + ",-" + Math.Max(Math.Max(sSymbol.Length, nMinWidth), sSymbol.ToString().Length) + "}";
            }
            sLine = String.Format(sFormat, arrObj);
            nLineLength = Math.Max(nLineLength, sLine.Length);
            sb.AppendLine(("").PadRight(nLineLength, '-'));
            sb.AppendLine(sLine);
            sb.AppendLine(("").PadRight(nLineLength, '-'));


            // construct the rows for transtion
            int nTransCount = 0;
            foreach (object objState in setAllState)
            {
                State state = (State)objState;
                arrObj[0] = (state.Equals(stateStart) ? ">" + state.ToString() : state.ToString());

                for (int i = 0; i < setAllSymbols.Count; i++)
                {
                    string sSymbol = setAllSymbols[i].ToString();

                    State[] arrStateTo = state.GetTransitions(sSymbol);
                    string sTo = String.Empty;
                    if (arrStateTo != null)
                    {
                        nTransCount += arrStateTo.Length;
                        sTo = arrStateTo[0].ToString();

                        for (int j = 1; j < arrStateTo.Length; j++)
                        {
                            sTo += ", " + arrStateTo[j].ToString();
                        }
                    }
                    else
                    {
                        sTo = "--";
                    }
                    arrObj[i + 1] = sTo;
                }

                sLine = String.Format(sFormat, arrObj);
                sb.AppendLine(sLine);
                nLineLength = Math.Max(nLineLength, sLine.Length);
            }

            sFormat = "State Count: {0}, Input Symbol Count: {1}, Transition Count: {2}";
            sLine = String.Format(sFormat, setAllState.Count, setAllSymbols.Count, nTransCount);
            nLineLength = Math.Max(nLineLength, sLine.Length);
            sb.AppendLine(("").PadRight(nLineLength, '-'));
            sb.AppendLine(sLine);
            nLineLength = Math.Max(nLineLength, sLine.Length);
            setAllSymbols.RemoveElement(MetaSymbol.EPSILON);

            return nLineLength;

        }


        /// <summary>
        /// Search and finds a match for the compiled pattern.
        /// One must call Compile method before calling this method.
        /// </summary>
        /// <param name="sSearchIn">String to search in.</param>
        /// <param name="nSearchStartAt">Index at which to begin the search.</param>
        /// <param name="nSearchEndAt">Index at which to end the search.</param>
        /// <param name="nFoundBeginAt">If match found, recives the index where the match started, otherwise -1</param>
        /// <param name="nFoundEndAt">If match found, recives the index where the match ended, otherwise -1</param>
        /// <returns>true if match found, otherwise false.</returns>
        public bool FindMatch(string sSearchIn,
                              int nSearchStartAt,
                              int nSearchEndAt,
                              ref int nFoundBeginAt,
                              ref int nFoundEndAt)
        {

            if (m_stateStartDfaM == null)
            {
                return false;
            }

            if (nSearchStartAt < 0)
            {
                return false;
            }

            State stateStart = m_stateStartDfaM;

            nFoundBeginAt = -1;
            nFoundEndAt = -1;

            bool bAccepted = false;
            State toState = null;
            State stateCurr = stateStart;
            int nIndex = nSearchStartAt;
            int nSearchUpTo = nSearchEndAt;


            while (nIndex <= nSearchUpTo)
            {

                if (m_bGreedy && IsWildCard(stateCurr) == true)
                {
                    if (nFoundBeginAt == -1)
                    {
                        nFoundBeginAt = nIndex;
                    }
                    ProcessWildCard(stateCurr, sSearchIn, ref nIndex, nSearchUpTo);
                }

                char chInputSymbol = sSearchIn[nIndex];

                toState = stateCurr.GetSingleTransition(chInputSymbol.ToString());

                if (toState == null)
                {
                    toState = stateCurr.GetSingleTransition(MetaSymbol.ANY_ONE_CHAR_TRANS);
                }

                if (toState != null)
                {
                    if (nFoundBeginAt == -1)
                    {
                        nFoundBeginAt = nIndex;
                    }

                    if (toState.AcceptingState)
                    {
                        if (m_bMatchAtEnd && nIndex != nSearchUpTo)  // then we ignore the accepting state
                        {
                            //toState = stateStart ;
                        }
                        else
                        {
                            bAccepted = true;
                            nFoundEndAt = nIndex;
                            if (m_bGreedy == false)
                            {
                                break;
                            }
                        }
                    }

                    stateCurr = toState;
                    nIndex++;
                }
                else
                {
                    if (!m_bMatchAtStart && !bAccepted)  // we reset everything
                    {
                        nIndex = (nFoundBeginAt != -1 ? nFoundBeginAt + 1 : nIndex + 1);

                        nFoundBeginAt = -1;
                        nFoundEndAt = -1;
                        //nIndex++;
                        stateCurr = stateStart;  // start from begining
                    }
                    else
                    {
                        break;
                    }
                }
            }  // end of while..loop 

            if (!bAccepted)
            {
                if (stateStart.AcceptingState == false)
                {
                    return false;
                }
                else // matched an empty string
                {
                    nFoundBeginAt = nSearchStartAt;
                    nFoundEndAt = nFoundBeginAt - 1;
                    return true;
                }
            }


            return true;
        }



        /// <summary>
        /// Determins if a state contains a wildcard transition.
        /// i.e., A_*B
        /// </summary>
        /// <param name="state">State to check</param>
        /// <returns>true if the state contains wildcard transition, otherwise false</returns>
        private bool IsWildCard(State state)
        {
            return (state == state.GetSingleTransition(MetaSymbol.ANY_ONE_CHAR_TRANS));
        }

        /// <summary>
        /// Process state that has wildcard transition.
        /// </summary>
        /// <param name="state">State with wildcard transition</param>
        /// <param name="sSearchIn">String to search in.</param>
        /// <param name="nCurrIndex">Current index of the search</param>
        /// <param name="nSearchUpTo">Index where to stop the search.</param>
        private void ProcessWildCard(State state, string sSearchIn, ref int nCurrIndex, int nSearchUpTo)
        {
            State toState = null;
            int nIndex = nCurrIndex;

            while (nIndex <= nSearchUpTo)
            {
                char ch = sSearchIn[nIndex];

                toState = state.GetSingleTransition(ch.ToString());

                if (toState != null)
                {
                    nCurrIndex = nIndex;
                }
                nIndex++;
            }

        }


        /// <summary>
        /// Get the ready state of the parser.
        /// </summary>
        /// <returns>true if a Compile method had been called successfully, otherwise false.</returns>
        public bool IsReady()
        {
            return (m_stateStartDfaM != null);
        }

        /// <summary>
        /// Position where error occured during the last compilation
        /// </summary>
        /// <returns>-1 if there was no compilation</returns>
        public int GetLastErrorPosition()
        {
            return m_nLastErrorIndex;
        }

        /// <summary>
        /// Indicate if the last compilation was successfull or error
        /// </summary>
        /// <returns></returns>
        public ErrorCode GetLastErrorCode()
        {
            return m_LastErrorCode;

        }
        /// <summary>
        /// Get last error length.
        /// </summary>
        /// <returns>Length</returns>
        public int GetLastErrorLength()
        {
            return m_nLastErrorLength;

        }

        /// <summary>
        /// Gets/Sets to indicate whether Find should stop at the first accepting state, 
        /// or should continue see if further match is possible (greedy).
        /// </summary>
        public bool UseGreedy
        {
            get
            {
                return m_bGreedy;
            }
            set
            {
                m_bGreedy = value;
            }

        }

        /// <summary>
        /// Helper fucntion. Updates the local variables once the validation returns.
        /// </summary>
        /// <param name="vi"></param>
        private void UpdateValidationInfo(ValidationInfo vi)
        {
            if (vi.ErrorCode == ErrorCode.ERR_SUCCESS)
            {
                m_bMatchAtEnd = vi.MatchAtEnd;
                m_bMatchAtStart = vi.MatchAtStart;
            }

            m_LastErrorCode = vi.ErrorCode;
            m_nLastErrorIndex = vi.ErrorStartAt;
            m_nLastErrorLength = vi.ErrorLength;
        }
    }

}


