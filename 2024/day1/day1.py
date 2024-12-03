def read_file(filename):
    left_values = []
    right_values = []
    
    with open(filename, 'r') as file:
        for line in file:
            left, right = line.strip().split()
            left_values.append(int(left))
            right_values.append(int(right))
            
    return left_values, right_values

valuesA, valuesB = read_file('input')
valuesA.sort()
valuesB.sort()

totalDist = 0
for x, y in zip(valuesA, valuesB):
    totalDist = totalDist + abs(x - y)

print(totalDist)