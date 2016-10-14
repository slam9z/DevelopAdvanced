class MyClass
	class << self
		def  my_method ; end
	end
end

obj=MyClass.new

p MyClass.eigenclass.superclass
