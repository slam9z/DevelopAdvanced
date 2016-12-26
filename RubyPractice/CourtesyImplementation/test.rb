# Ruby
class Node
end

class Box < Node
  def initialize 
    @children = []
  end
  # 完全看不懂的写法，而且还不能运行
  def << aNode
    @children<<aNode
  end
  def num_elephants
    result = 0
    @children.each do |c|
      if c.kind_of? Elephant
        result += 1
      else
        result += c.num_elephants
      end
    end
    return result
  end
end

class Elephant < Node
end

node=Box.new()
node.aNode(Elephant.new())
node.aNode(Elephant.new())

p node.num_elephants()