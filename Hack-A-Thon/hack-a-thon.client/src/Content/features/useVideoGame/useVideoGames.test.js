import { renderHook, act } from '@testing-library/react-hooks';
import { useVideoGames } from './useVideoGames';

// Mock fetch globally
beforeEach(() => {
  global.fetch = jest.fn();
});
afterEach(() => {
  jest.resetAllMocks();
});

describe('useVideoGames', () => {
  it('fetches and sets video games on mount', async () => {
    const mockGames = [{ id: 1, title: 'Test Game' }];
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => mockGames
    });

    const { result, waitForNextUpdate } = renderHook(() => useVideoGames());
    await waitForNextUpdate();
    expect(result.current.videoGames).toEqual(mockGames);
    expect(result.current.loading).toBe(false);
    expect(result.current.error).toBe(null);
  });

  it('handles fetch error', async () => {
    fetch.mockResolvedValueOnce({ ok: false });
    const { result, waitForNextUpdate } = renderHook(() => useVideoGames());
    await waitForNextUpdate();
    expect(result.current.videoGames).toEqual([]);
    expect(result.current.loading).toBe(false);
    expect(result.current.error).toBeInstanceOf(Error);
  });

  it('updates filters and fetches new data', async () => {
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => [{ id: 1, title: 'Game 1' }]
    });
    const { result, waitForNextUpdate } = renderHook(() => useVideoGames());
    await waitForNextUpdate();
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => [{ id: 2, title: 'Game 2' }]
    });
    act(() => {
      result.current.setFilters(f => ({ ...f, title: 'Game 2' }));
    });
    await waitForNextUpdate();
    expect(result.current.videoGames).toEqual([{ id: 2, title: 'Game 2' }]);
  });
});
