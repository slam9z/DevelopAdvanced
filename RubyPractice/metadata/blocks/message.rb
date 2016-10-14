message = "hello"
class << message
  def world
    puts 'hello world'
  end
end

p String.eigenclass