#mathjax公式编辑

##行内公式

行内公式可以与其他内容显示在同一行。
用两个美元符 $ 包裹住公式即可。
markdown原文:

$f(x)=ax+b$

markdown显示:

f(x)=ax+b

##行间公式

行间公式会占据单独的行。
公式前后各添加两个美元符 $。

markdown原文:

$$
f(x)=ax+b
$$

markdown显示:

f(x)=ax+b

##几个特殊符号

###^ 表示上标

markdown原文:

$a^2$

markdown显示:

a2
\
###_ 表示下标

由于markdown与mathjax的渲染有冲突，下划线必须使用反斜杠转义。

markdown原文:
$a\_2$
markdown显示:

a2

###{} 用于分组

举个例子，a的b乘c次方，需要对bc进行分组，否则就变成了a的b次方乘c。

markdown原文:
$a^{bc}$

##公式推导过程

有时一行放不下所有的推导过程，放到多行并使得每行的等号对齐可以大大增加可读性。
下面这个例子原始形式是A，然后经过三步推导最终得到了D。
以符号 & 的下一个字符进行对齐，末尾的三个反斜杠 \ 用来分割行。
这里是由于markdown与mathjax的渲染有冲突才需要用三个反斜杠。

markdown原文:
$$
\begin {aligned}
A&=B \\\
&=C \\\
&=D
\end {aligned}
$$


##分段函数

以符号 & 的下一个字符进行对齐，末尾的三个反斜杠 \ 用来分割行。

markdown原文:
$$
sign(x)=\begin {cases}
+1, & x\geq0 \\\
-1, & x<0
\end {cases}
$$

markdown显示:

sign(x)={
+1,
−1,
x≥0
x<0
sign(x)={+1,x≥0−1,x<0

##分数线的使用

\frac后面的两个分组分别作为分子和分母。

markdown原文:
$\frac{1}{1+e^{-x}}$

markdown显示:
1
1+
e
−x


##向量的使用

markdown原文:
$\vec{a}$
markdown显示:

a
⃗

##公式编辑常用字母符号

这些符号需要放在行内公式或行间公式之中。

###希腊字母

| 字母名称 | 大写| markdown原文| 小写| markdown原文|
|------|-------|-------------|-----|-------------|
| alpha| A     |    A|   α| \alpha|
| beta| B| B| B| β| β| \beta
| gamma| Γ| Γ| \Gamma| γ| γ| \gamma  |
| delta
| Δ
| Δ
| \Delta
| δ
| δ
| \delta
| eplison
| E
| E
| E
| ϵ
| ϵ
| \epsilon
| 
| 
| 
| ε
| ε
| \varepsilon
| zeta
| Z
| Z
| Z
| ζ
| ζ
| \zeta
| eta
| H
| H
| H
| η
| η
| \eta
| theta
| Θ
| Θ
| \Theta
| θ
| θ
| \theta
| iota
| I
| I
| I
| ι
| ι
| \iota
| kappa
| K
| K
| K
| κ
| κ
| \kappa
| lambda
| Λ
| Λ
| \Lambda
| λ
| λ
| \lambda
| mu
| M
| M
| M
| μ
| μ
| \mu
| nu
| N
| N
| N
| ν
| ν
| \nu
| xi
| Ξ
| Ξ
| \Xi
| ξ
| ξ
| \xi
| omicron
| O
| O
| O
| ο
| ο
| \omicron
| pi
| Π
| Π
| \Pi
| π
| π
| \pi
| 
| 
| 
| ϖ
| ϖ
| \varpi
| rho
| P
| P
| P
| ρ
| ρ
| \rho
| 
| 
| 
| ϱ
| ϱ
| \varrho
| sigma
| Σ
| Σ
| \Sigma
| σ
| σ
| \sigma
| 
| 
| 
| ς
| ς
| \varsigma
| tau
| T
| T
| T
| τ
| τ
| \tau
| upsilon
| Υ
| Υ
| \Upsilon
| υ
| υ
| \upsilon
| phi
| Φ
| Φ
| \Phi
| ϕ
| ϕ
| \phi
| 
| 
| 
| φ
| φ
| \varphi
| chi
| X
| X
| X
| χ
| χ
| \chi
| psi
| Ψ
| Ψ
| \Psi
| ψ
| ψ
| \psi
| omega
| Ω
| Ω
| \Omega
| ω
| ω
| \omega


###| 空心字母与Fraktur字母与花体字

| A-Z皆可使用
| 字体类型
| 符号
| markdown原文
| 空心
| A
| A
| \mathbb{A}
| Fraktur
| B
| B
| \mathfrak{B}
| 花体
| W
| W
\mathcal{W}


###括号

小括号与中括号可直接使用。
符号
markdown原文
{
{
\lbrace
}
}
\rbrace
⟨
⟨
\langle
⟩
⟩
\rangle
⌈
⌈
\lceil
⌉
⌉
\rceil
⌊
⌊
\lfloor
⌋
⌋
\rfloor

###数学符号


符号
markdown原文
∑
∑
\sum
∏
∏
\prod
∫
∫
\int
∬
∬
\iint
∭
∭
\iiint
∮
∮
\oint
∂
∂
\partial
∇
∇
\nabla
∞
∞
\infty
∼
∼
\sim
┐
⌝
\urcorner
⋅
⋅
\cdot
×
×
\times
∗
∗
\ast
÷
÷
\div
≤
≤
\leq
≥
≥
\geq
≠
≠
\neq
≈
≈
\approx
≡
≡
\equiv
≪
≪
\ll
≫
≫
\gg
∝
∝
\propto
∥
∥
\parallel
±
±
\pm
∓
∓
\mp
∅
∅
\emptyset
∈
∈
\in
∉
∉
\notin
⊆
⊆
\subseteq
⊇
⊇
\supseteq
⫋
⫋
\subsetneqq
⫌
⫌
\supsetneqq
⋂
⋂
\bigcap
⋃
⋃
\bigcup
查看更多LaTex符号
