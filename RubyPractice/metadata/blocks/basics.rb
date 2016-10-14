def a_method(a,b)
	p a+yield(a,b)
end

a_method(1,2) {|x,y|(x+y)*2}


def  block_method
	return yield if block_given?
	'no block'
end

p block_method
p block_method {"a block"}
