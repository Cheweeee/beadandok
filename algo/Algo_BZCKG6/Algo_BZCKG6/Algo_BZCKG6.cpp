#include <iostream>
#include <vector>
#include <queue>
#include <string>
#include <fstream>
#include <stdlib.h>

struct Edge {
    int v;		//current edge
    Edge* next;	//next edge
    float w;		//weight
    Edge(int vert, float weight) : v(vert), w(weight), next(nullptr) {}
};

bool graphFromFile(std::vector<Edge*> &A, std::string fileName) {	//returns true, if the file reading was successful
	std::string line;
	std::ifstream myfile(fileName);
	if (myfile.is_open()) {
		getline(myfile, line);
		int c = atoi(line.c_str());
		A.resize(c);
		int n = 0;
		while (getline(myfile, line)) {
			size_t pos = line.find(' ');
			std::string edge = line.substr(0,pos);	//the main edge
			std::string weight;
			A[n] = new Edge(atoi(edge.c_str()), 1.0f);
			auto t = A[n];
			line = line.substr(pos + 1);
			while (pos != -1) {			//read the rest of the line
				pos = line.find(' ');	//the next edge
				edge = line.substr(0, pos);
				line = line.substr(pos + 1);
				pos = line.find(' ');	//the weight to the next edge
				weight = line.substr(0, pos);
				t->next = new Edge(atoi(edge.c_str()), std::stof(weight.c_str()));  //stof is string to float
				t = t->next;
				line = line.substr(pos + 1);
			}
			++n;
		}
		myfile.close();
		return true;
	}
	else {
		std::cout << "Unable to open file";
		return false;
	}
}

bool containsQueue(std::queue<int> Q, int v) {
	std::queue<int> ret = Q;
	while (!ret.empty()) {
		if (ret.front() == v) {
			return true;
		}
		ret.pop();
		return false;
	}
}

std::vector<int> Bellmann_Ford(const std::vector<Edge*>& A, int s) {		//returns the parents' vector for the best way
	std::vector<float> d(A.size());
	std::vector<int> parent(A.size());
	for (auto i = 0; i < A.size(); ++i) {
		d[i] = -1.0f;	//there isn't negative weight
		parent[i] = -1;	//it's instead off nullptr
	}
	d[s] = 1.0f;		//because of multiplication
	std::queue<int> Q;
	Q.push(s);
	while (!Q.empty()) {
		int u = Q.front();
		Q.pop();
		Edge* e = A[u];
		while (e != nullptr) {
			if (d[e->v] < d[u] * e->w) {		//the bigger, the more reliable
				int v = e->v;
				d[v] = d[u] * e->w;
				parent[v] = u;
				if (!containsQueue(Q, v)) {
					Q.push(v);
				}
			}
			e = e->next;
		}
	}
	return parent;
}

void bestWay(const std::vector<Edge*>& A, int u, int v) {
	if (u < 0 || u>=A.size() || v<0 || v>=A.size()) {
		std::cout << "There are no such points!" << std::endl;
		return;
	}
	if (u == v) {
		std::cout << "The starting point and the destination are the same!" << std::endl;
		return;
	}
	std::vector<int> parent = Bellmann_Ford(A, u);

	std::vector<int> best;
	best.push_back(v);
	int i = v;
	while (parent[i] != u) {
		if (parent[i] == -1) {
			std::cout << "There is no connection between " << u << " and " << v << std::endl;
			return;
		}
		i = parent[i];
		best.push_back(i);
	}
	std::cout << "The most reliable connection from " << u << " to " << v << " is:\n" << u << " ";
	while (!best.empty()) {
		int f = best.back();
		best.pop_back();
		std::cout << f << " ";
	}
	std::cout << std::endl;
}

int main() {
	std::vector<Edge*> B;
	//normal graph with 6 edges
	if (!graphFromFile(B, "graph1.txt")) {
		return 0;
	}

	//bigger graph with 10 edges, edge with no connection, edges with only outgoing connections
	//graphFromFile(B, "graph2.txt");

	//small graph with 4 edges, there is a parallel connection (for testing)
	//graphFromFile(B, "graph3.txt");
	
	bool isOn = true;
	std::string from;
	std::string to;
	std::string answer;
	do {
		std::cout << "Please give me the locations!" << std::endl << "From: ";
		std::cin >> from;
		std::cout << "To: ";
		std::cin >> to;
		bestWay(B, atoi(from.c_str()), atoi(to.c_str()));
		std::cout << "Another? (y/n)";
		std::cin >> answer;
		isOn = answer == "y";
	} while (isOn);
    return 0;
}