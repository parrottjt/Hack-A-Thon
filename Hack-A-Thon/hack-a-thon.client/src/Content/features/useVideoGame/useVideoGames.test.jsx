import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import { useVideoGames } from './useVideoGames';

// Mock fetch globally
beforeEach(() => {
  global.fetch = jest.fn();
});
afterEach(() => {
  jest.resetAllMocks();
});

function TestComponent({ initialFilters }) {
  const { videoGames, loading, error, filters, setFilters } = useVideoGames(initialFilters);
  React.useEffect(() => {}, []); // force effect run for test
  return (
    <div>
      {loading && <span data-testid="loading">Loading...</span>}
      {error && <span data-testid="error">Error!</span>}
      <ul data-testid="games-list">
        {videoGames.map(game => <li key={game.id}>{game.title}</li>)}
      </ul>
      <button onClick={() => setFilters(f => ({ ...f, title: 'Mario' }))}>Filter Mario</button>
    </div>
  );
}

describe('useVideoGames (React 19 compatible)', () => {
  it('handles explicit "Failed to fetch video games" error', async () => {
    fetch.mockResolvedValueOnce({ ok: false });
    let errorMsg = '';
    function ErrorTest() {
      const { error } = useVideoGames();
      if (error) errorMsg = error.message;
      return null;
    }
    render(<ErrorTest />);
    await waitFor(() => expect(errorMsg).toBe('Failed to fetch video games'));
  });
  it('returns initial state correctly', async () => {
    fetch.mockResolvedValueOnce({ ok: true, json: async () => [] });
    render(<TestComponent />);
    expect(screen.getByTestId('loading')).toBeInTheDocument();
    await waitFor(() => expect(screen.getByTestId('games-list')).toBeInTheDocument());
    expect(screen.queryByTestId('error')).not.toBeInTheDocument();
  });

  it('handles error object with message', async () => {
    fetch.mockRejectedValueOnce(new Error('Network down'));
    render(<TestComponent />);
    await waitFor(() => expect(screen.getByTestId('error')).toBeInTheDocument());
  });

  it('handles empty result', async () => {
    fetch.mockResolvedValueOnce({ ok: true, json: async () => [] });
    render(<TestComponent />);
    await waitFor(() => expect(screen.getByTestId('games-list')).toBeInTheDocument());
    expect(screen.queryByText('Test Game')).not.toBeInTheDocument();
  });

  it('handles fetch throwing synchronously', async () => {
    fetch.mockImplementationOnce(() => { throw new Error('Sync fail'); });
    render(<TestComponent />);
    await waitFor(() => expect(screen.getByTestId('error')).toBeInTheDocument());
  });

  it('calls correct URL with filters', async () => {
    fetch.mockResolvedValueOnce({ ok: true, json: async () => [] });
    const filters = { title: 'Halo', developer: 'Bungie', publisher: '', genre: '', esrbRating: '' };
    render(<TestComponent initialFilters={filters} />);
    await waitFor(() => {
      expect(fetch).toHaveBeenCalledWith(expect.stringContaining('title=Halo'));
      expect(fetch).toHaveBeenCalledWith(expect.stringContaining('developer=Bungie'));
    });
  });

  it('setFilters edge case: same value does not refetch', async () => {
    fetch.mockResolvedValue({ ok: true, json: async () => [] });
    let setFiltersFn;
    function EdgeTest() {
      const { setFilters } = useVideoGames();
      setFiltersFn = setFilters;
      return null;
    }
    render(<EdgeTest />);
    // Wait for initial fetch
    await waitFor(() => expect(fetch).toHaveBeenCalledTimes(1));
    // Now call setFilters with same value
    await import('react').then(({ act }) => act(() => {
      setFiltersFn(f => ({ ...f }));
    }));
    // Wait a tick to ensure no extra fetch
    await new Promise(r => setTimeout(r, 10));
    expect(fetch).toHaveBeenCalledTimes(1);
  });
  it('fetches and displays video games', async () => {
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => [{ id: 1, title: 'Test Game' }]
    });
    render(<TestComponent />);
    expect(screen.getByTestId('loading')).toBeInTheDocument();
    await waitFor(() => expect(screen.getByText('Test Game')).toBeInTheDocument());
  });

  it('handles fetch error', async () => {
    fetch.mockResolvedValueOnce({ ok: false });
    render(<TestComponent />);
    await waitFor(() => expect(screen.getByTestId('error')).toBeInTheDocument());
  });

  it('updates filters and fetches new data', async () => {
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => [{ id: 1, title: 'Game 1' }]
    });
    render(<TestComponent />);
    await waitFor(() => expect(screen.getByText('Game 1')).toBeInTheDocument());
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => [{ id: 2, title: 'Mario' }]
    });
    screen.getByText('Filter Mario').click();
    await waitFor(() => expect(screen.getByText('Mario')).toBeInTheDocument());
  });
});
