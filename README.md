# Squares
Solution to find all squares in given points list.

# Algorithm

  - Assume given points is an array of object Point, each Point has x,y.
  - First iterate through the array and add each item into an HashSet
  - Using Math, Say vertices A, B, C, D can form a square, AC is known and it's a diagonal line, then the corresponding B, D is unique. 
  - Iterate with a for-i-loop and a for-j-inner-loop. Say input[i] and input[j] form a diagonal line, find its anti-diagonal line in the set: If exist, we found square.
  - Check if square is unique and add it to list.
  - Should be O(n^2)

# Solution
  - Contains API Ednpoint for UI to interact.
  - Entity framework for database access
  - Persistance DAL layer.
  - Square calclulator

# Running
  - Run solution with Visual Studio

