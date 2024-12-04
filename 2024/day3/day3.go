package main

import (
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
	"unicode/utf8"
)

func main() {
	content, err := os.ReadFile("input")
	if err != nil {
		log.Fatal(err)
	}
	inputStr := string(content)

	sum := 0
	for i, _ := range inputStr {
		remain := inputStr[i:]
		if utf8.RuneCountInString(remain) < 4 {
			break
		}

		if remain[:4] != "mul(" {
			continue
		}

		beginIndex := strings.Index(remain, "(")
		endIndex := strings.Index(remain, ")")
		if beginIndex == -1 || endIndex == -1 {
			break
		}
		if endIndex < beginIndex {
			continue
		}

		insideParens := remain[beginIndex+1 : endIndex]

		if !strings.Contains(insideParens, ",") {
			continue
		}

		numbers := []int{}
		numStrs := strings.Split(insideParens, ",")

		isErr := false
		for _, numStr := range numStrs {
			if num, err := strconv.Atoi(numStr); err == nil {
				numbers = append(numbers, num)
			} else {
				isErr = true
				continue
			}
		}
		if isErr || len(numbers) != 2 {
			continue
		}
		sum = sum + numbers[0]*numbers[1]
	}
	fmt.Println(sum)
}
