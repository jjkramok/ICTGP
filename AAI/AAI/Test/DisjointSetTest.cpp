#include "../Program.h"

#if UnitTest == 1

#include "../lib/catch.hpp"
#include "../Week1/DisjointSet.h"

#pragma once

TEST_CASE("DisjointSet Constructor") {
	DisjointSet set(2);

	REQUIRE(set.setArray[0] == -1);
	REQUIRE(set.setArray[1] == -1);
}

TEST_CASE("DisjointSet Union") {
	DisjointSet set(2);

	REQUIRE(set.Union(0, 1) == 1);

	REQUIRE(set.setArray[0] == 1);
	REQUIRE(set.setArray[1] == -2);
}

TEST_CASE("DisjointSet Double Union") {
	DisjointSet set(3);

	REQUIRE(set.Union(0, 1) == 1);
	REQUIRE(set.Union(0, 2) == 1);
	REQUIRE(set.Union(1, 2) == 0);

	REQUIRE(set.setArray[0] == 1);
	REQUIRE(set.setArray[1] == 2);
	REQUIRE(set.setArray[2] == -3);
}

TEST_CASE("DisjointSet Find") {
	DisjointSet set(4);

	REQUIRE(set.Union(0, 1) == 1);
	REQUIRE(set.Union(0, 2) == 1);
	REQUIRE(set.Union(1, 2) == 0);

	REQUIRE(set.Find(0) == 2);
	REQUIRE(set.Find(1) == 2);
	REQUIRE(set.Find(2) == 2);
	REQUIRE(set.Find(3) == 3);
}

#endif