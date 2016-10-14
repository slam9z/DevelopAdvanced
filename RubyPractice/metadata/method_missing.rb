class Lawper
	def method_missing(method,*args)
		p "you called: #{method} #{args.join(',')}"
	end
end 	

bob=Lawper.new
bob.sim("234",23,234)