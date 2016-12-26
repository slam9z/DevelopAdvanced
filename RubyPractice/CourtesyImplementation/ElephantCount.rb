class Node
  #if this is a strongly typed language I define an abstract
  #num_elephants here
end

class Box < Node
  def initialize 
    @children = []
  end
  def  aNode  node
      @children.push(node)
  end
  def num_elephants
    result = 0
    @children.each do |c|
      result += c.num_elephants
    end
    return result
  end
end

class Elephant < Node
  def num_elephants
    return 1
  end
end


node=Box.new()
node.aNode(Elephant.new())
node.aNode(Elephant.new())

p node.num_elephants()