class Node:
    def __init__(self, value, left_child = None, right_child = None):
        self.val = value
        self.left = left_child
        self.right = right_child

    def __str__(self):
        return str(self.val)
    
def height(node):
    i = 0      
    if node is None:
        return 0
    else:
        left_height = height(node.left)
        right_height = height(node.right)
        return max(left_height, right_height) + 1

n1 = Node(1)
n2 = Node(2)
n3 = Node(3)
n4 = Node(4)
n5 = Node(5)
n6 = Node(6)
n7 = Node(7)
n1.left = n2
n1.right = n3
n2.left = n5
n2.right = n6
n6.left = n7
n3.right = n4
print(height(n1))