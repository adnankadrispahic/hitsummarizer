import { useCallback, useState } from "react";
import "./App.css";
import { SearchResult } from "./types/types";

function App() {
  const [query, setQuery] = useState<string>("");
  const [results, setResults] = useState<SearchResult[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");

  const handleSubmit = useCallback(
    async (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();

      try {
        setErrorMessage("");
        setIsLoading(true);

        const response = await fetch(
          `http://localhost:8080/api/search?query=${query}`
        );

        if (!response.ok) {
          throw new Error("Failed to fetch data from server");
        }

        const searchResults: SearchResult[] = await response.json();

        setResults(searchResults);
      } catch (e: unknown) {
        console.log(e);
        setErrorMessage("Error occured while fetching data");

        setResults([]);
      } finally {
        setIsLoading(false);
      }
    },
    [query]
  );

  return (
    <>
      <div className="search-container">
        <section>
          <div className="section-header">
            <h2>Enter your search words</h2>
          </div>
          <form className="search-form" onSubmit={handleSubmit}>
            <div className="search-bar">
              <input
                className="search-input"
                type="text"
                placeholder="Search..."
                onChange={(e) => setQuery(e.target.value)}
              />
              <button>Search</button>
            </div>
          </form>
        </section>

        {isLoading && <div>Loading...</div>}
        {errorMessage && <section>{errorMessage}</section>}

        {!isLoading && results.length > 0 && (
          <section className="search-results">
            <div>
              <h3>Search Results</h3>
            </div>
            <table className="search-results-table">
              <thead>
                <tr>
                  <th>Search Engine</th>
                  <th>Total Results</th>
                </tr>
              </thead>
              <tbody>
                {results.map((r: SearchResult) => (
                  <tr>
                    <td>{r.engineName}</td>
                    <td>{r.totalHits}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </section>
        )}
      </div>
    </>
  );
}

export default App;
