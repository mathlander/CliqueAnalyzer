# CliqueAnalyzer
This project represents a proof of concept for an optimal algorithm which solves
the deterministic clique problem.  That is, given a network of N nodes, does there
exist a clique of size Q?

Analysis on the worst case runtime for such an optimal solution is available here:

http://arxiv.org/abs/1601.03619

The algorithm itself generates all of the combinations "N choose Q", where Q
represents the number of members included in the clique.  All cliques of size Q
may be iterated via the static method FindCliquesBySize() in CliqueAnalyzer.CliqueSolver.

An additional method, CliqueAnalyzer.CliqueSolver.FindLargestClique() is available,
which returns the collection of all cliques of maximal size.

For now, the program is mostly of theoretical importance, as it is a realization of
the optimal algorithm described in the publication referenced above.  However,
this is an active project and feedback is welcome.

Near term enhancements for this project will include:

1. (feature added) the ability to construct a matrix by adding relationships between nodes
programmatically, rather than relying on a csv file;
2. the ability to search for the largest clique involving a specific node; and
3. the ability to search for the largest clique involving a subnetwork, i.e.
constructing a maximal clique from some smaller clique.
