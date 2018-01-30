#include "DisjointSet.h"


DisjointSet::DisjointSet()
{
	
}

DisjointSet::~DisjointSet()
{
	free(this->setArray);
}

void DisjointSet::Init(int size)
{
	setArray = (int*)calloc(size, sizeof(int));
	setArraySize = size;

	for (int i = 0; i < setArraySize; i++) {
		setArray[i] = -1;
	}
}

void DisjointSet::Union(int element1, int element2)
{
	int parent1 = this->Find(element1);
	int parent2 = this->Find(element2);

	if (parent1 == parent2)
	{
		return;
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

	free(elementsToChange);

	return element;
}

char * DisjointSet::ToString()
{
	char *result = (char*)calloc(setArraySize * 5 + 1, sizeof(char));

	for (int i = 0; i < setArraySize; i++) {
		sprintf(&result[i * 5], "%d", setArray[i]);

		for (int j = i * 5; j < i * 5 + 5; j++) {
			if (result[j] == '\0') {
				result[j] = ' ';
			}
		}
	}
	result[setArraySize * 5 + 1] = '\0';

	return result;
}