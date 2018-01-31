#include "DisjointSet.h"


DisjointSet::DisjointSet(int size)
{
	setArray = (int*)calloc(size, sizeof(int));
	
	setArraySize = size;
	setCount = size;

	for (int i = 0; i < setArraySize; i++) {
		setArray[i] = -1;
	}
}

DisjointSet::~DisjointSet()
{
	free(this->setArray);
}

int DisjointSet::Union(int element1, int element2)
{
	int parent1 = this->Find(element1);
	int parent2 = this->Find(element2);

	if (parent1 == parent2)
	{
		return 1;
	}

	if (setArray[parent1] > setArray[parent2])
	{
		setArray[parent1] += setArray[parent2];
		setArray[parent2] = parent1;
	}
	else
	{
		setArray[parent2] += setArray[parent1];
		setArray[parent1] = parent2;
	}
	setCount--;
	return 0;
}

int DisjointSet::Find(int element)
{
	int *elementsToChange = (int *)calloc(setArraySize, sizeof(int));
	int i = -1;
	while (setArray[element] >= 0)
	{
		// Keep track of elements that need to be changed to reduce the tree height.
		i++;
		elementsToChange[i] = element;

		// Find the actual parent.
		element = setArray[element];
	}

	// Reduce height of all elements in the specific tree that are encounterd.
	for (; i >= 0; i--) {
		setArray[elementsToChange[i]] = element;
	}
	try {
		free(elementsToChange);
	}
	catch(std::exception *e){
		std::cout << e;
	}
	return element;
}

char * DisjointSet::ToString()
{
	// Maybe use std::string
	char *result = (char*)calloc(setArraySize * 5, sizeof(char));

	for (int i = 0; i < setArraySize; i++) {
		sprintf(&result[i * 5], "%d", setArray[i]);

		for (int j = i * 5; j < i * 5 + 5; j++) {
			if (result[j] == '\0') {
				result[j] = ' ';
			}
		}
	}
	result[setArraySize * 5] = '\0';

	return result;
}