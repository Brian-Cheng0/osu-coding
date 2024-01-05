# search.py
# ---------
# Licensing Information: Please do not distribute or publish solutions to this
# project. You are free to use and extend these projects for educational
# purposes. The Pacman AI projects were developed at UC Berkeley, primarily by
# John DeNero (denero@cs.berkeley.edu) and Dan Klein (klein@cs.berkeley.edu).
# For more info, see http://inst.eecs.berkeley.edu/~cs188/sp09/pacman.html

"""
In search.py, you will implement generic search algorithms which are called
by Pacman agents (in searchAgents.py).
"""

import util
from util import heappush, heappop
class SearchProblem:
    """
    This class outlines the structure of a search problem, but doesn't implement
    any of the methods (in object-oriented terminology: an abstract class).

    You do not need to change anything in this class, ever.
    """

    def getStartState(self):
      """
      Returns the start state for the search problem
      """
      util.raiseNotDefined()

    def isGoalState(self, state):
      """
      state: Search state

      Returns True if and only if the state is a valid goal state
      """
      util.raiseNotDefined()

    def getSuccessors(self, state):
      """
      state: Search state

      For a given state, this should return a list of triples,
      (successor, action, stepCost), where 'successor' is a
      successor to the current state, 'action' is the action
      required to get there, and 'stepCost' is the incremental
      cost of expanding to that successor
      """
      util.raiseNotDefined()

    def getCostOfActions(self, actions):
      """
      actions: A list of actions to take

      This method returns the total cost of a particular sequence of actions.  The sequence must
      be composed of legal moves
      """
      util.raiseNotDefined()


def tinyMazeSearch(problem):
    """
    Returns a sequence of moves that solves tinyMaze.  For any other
    maze, the sequence of moves will be incorrect, so only use this for tinyMaze
    """
    from game import Directions
    s = Directions.SOUTH
    w = Directions.WEST
    return  [s,s,w,s,w,w,s,w]

def depthFirstSearch(problem):
    """
    Search the deepest nodes in the search tree first.
    Your search algorithm needs to return a list of actions that reaches
    the goal. Make sure that you implement the graph search version of DFS,
    which avoids expanding any already visited states. 
    Otherwise your implementation may run infinitely!
    To get started, you might want to try some of these simple commands to
    understand the search problem that is being passed in:
    print("Start:", problem.getStartState())
    print("Is the start a goal?", problem.isGoalState(problem.getStartState()))
    print("Start's successors:", problem.getSuccessors(problem.getStartState()))
    """
    """
    YOUR CODE HERE
    """

    closedset=[]
    #openset = [problem.getStartState()]
    edge = util.Stack()
    #make the first state in the stack
    edge.push((problem.getStartState(),[]))
    #put in the loop to find the right way to the final position
    while edge.__sizeof__()>0:
        (state,path) = edge.pop()
        if problem.isGoalState(state):
          return path
          #while state!=problem.getStartState():
           #  return(problem.getSuccessors(state))
        closedset.append(state)
        for next_state,action,cost in problem.getSuccessors(state):
          #avoid appending the state twice
          if next_state not in closedset:
              edge.push((next_state,path+[action]))

    """for(next_state, action) in problem.getSuccessors(state):
              if next_state not in closedset:
                closedset.append(state)
                openset.append(next_state) 
                problem.getSuccessors(problem.getStartState())"""
          
    util.raiseNotDefined()
    

def breadthFirstSearch(problem):
    """
    YOUR CODE HERE
    """
    closedset=[]
    #openset = [problem.getStartState()]
    edge = util.Queue()
    #make the first state in the Queue
    edge.push((problem.getStartState(),[]))
    #put in the loop to find the right way to the final position
    while edge.__sizeof__()>0:
        (state,path) = edge.pop()
        if state in closedset:
           continue
        elif problem.isGoalState(state):
          return path
          #while state!=problem.getStartState():
           #  return(problem.getSuccessors(state))
        closedset.append(state)
        for next_state,action,cost in problem.getSuccessors(state):
          #avoid appending the state twice
          if next_state not in closedset:
              edge.push((next_state,path+[action]))
    util.raiseNotDefined()
    

def uniformCostSearch(problem):
    """
    YOUR CODE HERE
    """
    closedset=[]
    #openset = [problem.getStartState()]
    edge = util.PriorityQueue()
    #make the first state in the PriorityQueue
    edge.push((problem.getStartState(),[],0),0)
    #put in the loop to find the right way to the final position
    while edge.__sizeof__()>0:
        (state,path, now_cost) = edge.pop()
        if state in closedset:
           continue
        elif problem.isGoalState(state):
          return path
          #while state!=problem.getStartState():
           #  return(problem.getSuccessors(state))
        closedset.append(state)
        for next_state,action,cost in problem.getSuccessors(state):
          #avoid appending the state twice
          if next_state not in closedset:
              edge.push((next_state,path+[action],now_cost+cost),now_cost+cost)
    util.raiseNotDefined()

def nullHeuristic(state, problem=None):
    """
    A heuristic function estimates the cost from the current state to the nearest
    goal in the provided SearchProblem.  This heuristic is trivial.
    """
    return 0

def aStarSearch(problem, heuristic=nullHeuristic):
    """
    YOUR CODE HERE
    """
    closedset=[]
    #openset = [problem.getStartState()]
    edge = util.PriorityQueue()
    #make the first state in the PriorityQueue
    edge.push((problem.getStartState(),[],0),0)
    #put in the loop to find the right way to the final position
    while edge.__sizeof__()>0:
        (state,path, now_cost) = edge.pop()
        if state in closedset:
           continue
        elif problem.isGoalState(state):
          return path
          #while state!=problem.getStartState():
           #  return(problem.getSuccessors(state))
        closedset.append(state)
        for next_state,action,cost in problem.getSuccessors(state):
          #avoid appending the state twice
          if next_state not in closedset:
              edge.push((next_state,path+[action],now_cost+cost),now_cost+cost+heuristic(next_state,problem))
    util.raiseNotDefined()


# Abbreviations
bfs = breadthFirstSearch
dfs = depthFirstSearch
astar = aStarSearch
ucs = uniformCostSearch
