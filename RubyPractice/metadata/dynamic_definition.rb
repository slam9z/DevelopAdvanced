class DynamicMethoad
	define_method  :dynamicMethod do |arg|
	p arg*3
	end
end

obj=DynamicMethoad.new
obj.dynamicMethod(23)