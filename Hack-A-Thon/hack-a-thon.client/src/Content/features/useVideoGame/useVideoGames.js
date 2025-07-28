import { useState, useEffect, useRef } from "react";

export function useVideoGames(initialFilters = {
  title: '',
  developer: '',
  publisher: '',
  genre: '',
  esrbRating: ''
}) {
  const [filters, setFilters] = useState(initialFilters);
  const [videoGames, setVideoGames] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchGames = async (filters) => {
    setLoading(true);
    setError(null);
    try {
      const qs = new URLSearchParams(filters).toString();
      const url = qs ? `https://localhost:7042/VideoGame?${qs}` : 'https://localhost:7042/VideoGames';
      const res = await fetch(url);
      if (!res.ok) throw new Error("Failed to fetch video games");
      setVideoGames(await res.json());
    } catch (e) {
      setError(e);
      setVideoGames([]);
    } finally {
      setLoading(false);
    }
  };


  // Shallow equality check for filters
  const prevFilters = useRef(null);
  function areFiltersEqual(a, b) {
    return Object.keys(a).length === Object.keys(b).length &&
      Object.keys(a).every(key => a[key] === b[key]);
  }

  useEffect(() => {
    if (prevFilters.current === null || !areFiltersEqual(filters, prevFilters.current)) {
      prevFilters.current = filters;
      fetchGames(filters);
    }
    // eslint-disable-next-line
  }, [filters]);


  // Update a video game (accepts id and updatedData)
  const updateGame = async (id, updatedData) => {
    setLoading(true);
    setError(null);
    try {
      const payload = { ...updatedData, Id: id };
      const res = await fetch(`https://localhost:7042/Update/`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      if (!res.ok) throw new Error("Failed to update video game");
      await fetchGames(filters); // Refresh list
    } catch (e) {
      setError(e);
    } finally {
      setLoading(false);
    }
  };

  // Delete a video game (expects id as argument)
  const deleteGame = async (id) => {
    setLoading(true);
    setError(null);
    try {
      const res = await fetch(`https://localhost:7042/Delete/`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id }),
      });
      if (!res.ok) throw new Error("Failed to delete video game");
      await fetchGames(filters); // Refresh list
    } catch (e) {
      setError(e);
    } finally {
      setLoading(false);
    }
  };

  return {
    videoGames,
    loading,
    error,
    filters,
    setFilters,
    updateGame,
    deleteGame,
  };
}
