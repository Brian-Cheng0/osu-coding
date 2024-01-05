from collections import deque

def bfs(graph, start=0):
    total = len(graph)
    q = deque()
    q.append(start)
    distances = [None] * total
    distances[start] = 0
    while q:
        current = q.popleft()
        for neighbor in range(total):
            if graph[current][neighbor] == 1 and distances[neighbor] is None:
                    distances[neighbor] = distances[current] + 1
                    q.append(neighbor)
    
    return distances
