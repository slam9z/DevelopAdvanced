class Myclass 
	def my_method(arg)
		p arg*2
	end
end
 
obj=Myclass.new
obj.my_method(3)

obj.send(:my_method,3) 
# obj.send(my_method,3) 