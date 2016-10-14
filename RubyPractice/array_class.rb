name =%w(wei li xin)
name2 =%w(123 123 233 xin)

p (name+name2)
p (name|name2)
p (name-name2)

p name

p name[1...2]

num=[1,23,23]
p num.collect{|item|item*2}
p num

num2=[1,23,23]
p num2.collect!{|item|item*2}
p num2