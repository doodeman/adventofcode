def read_file(filename):
    lines = []
    
    with open(filename, 'r') as file:
        for line in file:
            char_array = list(line.rstrip())
            lines.append(char_array)
            
    return lines

def isXmas(word):
    return word == "XMAS" or word == "SAMX"

lines = read_file("input")

words = []

xWidth = len(lines[0])
yWidth = len(lines)

#Horizontal scan 
for y, line in enumerate(lines): 
    for x, char in enumerate(line): 
        if x + 3 >= xWidth:
            continue
        word = line[x] + line[x+1] + line[x+2] + line[x+3]
        if isXmas(word):
            words.append(line[x] + line[x+1] + line[x+2] + line[x+3])

#Vertical scan
for x in range(xWidth):
    for y in range(yWidth): 
        if y + 3 >= yWidth: 
            continue
        word = lines[y][x] + lines[y+1][x] + lines[y+2][x] + lines[y+3][x]
        if isXmas(word):
            words.append(word)

#Diagonal scan (right)
for x in range(xWidth):
    for y in range(yWidth): 
        if y + 3 >= yWidth or x + 3 >= xWidth: 
            continue 
        word = lines[y][x] + lines[y+1][x+1] + lines[y+2][x+2] + lines[y+3][y+3]
        if isXmas(word):
            words.append(word)

#Diagonal scan (left)
for x in range(xWidth-1, 0, -1):
    for y in range(yWidth): 
        if y + 3 >= yWidth or x - 3 < 0: 
            continue 
        word = lines[y][x] + lines[y+1][x-1] + lines[y+2][x-2] + lines[y+3][y-3]
        if isXmas(word):
            words.append(word)

print(len(words))