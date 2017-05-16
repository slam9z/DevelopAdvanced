[Understanding ASP.NET View State](https://msdn.microsoft.com/en-us/library/ms972976.aspx)


##The ASP.NET Page Life Cycle

###Events in the Page Life Cycle

![](https://i-msdn.sec.s-msft.com/dynimg/IC152667.gif)

####Stage 0 - Instantiation

```aspx

<html>
<body>
  <h1>Welcome to my Homepage!</h1>
  <form runat="server">
    What is your name?
    <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
    <br />What is your gender?
    <asp:DropDownList runat="server" ID="ddlGender">
      <asp:ListItem Select="True" Value="M">Male</asp:ListItem>
      <asp:ListItem Value="F">Female</asp:ListItem>
      <asp:ListItem Value="U">Undecided</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Button runat="server" Text="Submit!"></asp:Button>
  </form>
</body>
</html>

```

![](https://i-msdn.sec.s-msft.com/dynimg/IC43087.gif)


####Stage 1 - Initialization

####Stage 2 - Load View State

####Stage 3 - Load Postback Data

####Stage 4 - Load

####Stage 5 - Raise Postback Event

####Stage 6 - Save View State

####Stage 7 - Render


##The Role of View State

View state's purpose in life is simple: it's there to persist state across postbacks. 
(For an ASP.NET Web page, its state is the property values of the controls that make up its control hierarchy.) 
This begs the question, "What sort of state needs to be persisted?" 
To answer that question, let's start by looking at what state doesn't need to be persisted across postbacks.
 Recall that in the instantiation stage of the page life cycle, the control hierarchy is created and those 
properties that are specified in the declarative syntax are assigned. 
Since these declarative properties are automatically reassigned on each postback when the control hierarchy is constructed,
 there's no need to store these property values in the view state.


##View State and Dynamically Added Controls


##The ViewState Property


##Timing the Tracking of View State


##Storing Information in the Page's ViewState Property


##The Cost of View State


##Disabling the View State


The EnableViewState property is defined in the System.Web.UI.Control class, so all server controls have this property,
 including the Page class. You can therefore indicate that an entire page's view state need not be saved by setting the Page 
class's EnableViewState to False. 
(This can be done either in the code-behind class with Page.EnableViewState = false;
 or as a @Page-level directive—<%@Page EnableViewState="False" %>.)

##Specifying Where to Persist the View State


##Parsing the View State

