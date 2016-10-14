class HelloBase
	attr_accessor :base,:y

	def initialize
	   @base="base"
	end
end




class  HelloWorld<HelloBase
    def initialize(name="wei")
	@name=name
	@@lastname=""
	end
	
	def self.HelloWorld
		p @@lastname
	end
	
	def self.lastname
		return @@lastname
	end
	
	def self.lastname=(att)
	@@lastname=att
	end
	
	
	def	hello
		print "hello world , I am ",@name,@base
	end
	public:hello
#	private:hello
	
	def name
	  return @name
	end
	
	def name=(att)
		@name=att
	end 
end

class  HelloWorld
	def say(msg)
	p msg,@name
	end
end

wei=HelloWorld.new("wei li")
wei.hello
wei.name="ssss"
p wei.name
wei.hello

HelloWorld.lastname="1233"
HelloWorld.HelloWorld

wei.say("baby")