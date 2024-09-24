export type SearchResult = {
  engineName: string;
  totalHits: number;
  errors?: Error[];
};

export type Error = {
  message: string;
}