import '@testing-library/jest-dom';

import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import Content from './Content';


jest.mock('../features/videoGames/useVideoGames');
import { useVideoGames } from '../features/videoGames/useVideoGames';

describe('Content', () => {
  afterEach(() => {
    jest.resetAllMocks();
  });

  it('renders filter inputs and video game cards', () => {
    useVideoGames.mockImplementation(() => ({
      videoGames: [
        { id: 1, title: 'Halo', developer: 'Bungie', publisher: 'Microsoft', genre: 'Shooter', esrbRating: 'M' }
      ],
      loading: false,
      error: null,
      filters: { title: '', developer: '', publisher: '', genre: '', esrbRating: '' },
      setFilters: jest.fn()
    }));
    render(<Content />);
    expect(screen.getByPlaceholderText(/search by title/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/search by developer/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/search by publisher/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/search by genre/i)).toBeInTheDocument();
    expect(screen.getByText(/all ratings/i)).toBeInTheDocument();
    expect(screen.getByText('Halo')).toBeInTheDocument();
  });

  it('shows loading state', () => {
    useVideoGames.mockImplementation(() => ({
      videoGames: [],
      loading: true,
      error: null,
      filters: { title: '', developer: '', publisher: '', genre: '', esrbRating: '' },
      setFilters: jest.fn()
    }));
    render(<Content />);
    expect(screen.getByText(/loading... please wait/i)).toBeInTheDocument();
  });

  it('shows error state', () => {
    useVideoGames.mockImplementation(() => ({
      videoGames: [],
      loading: false,
      error: { message: 'Network error' },
      filters: { title: '', developer: '', publisher: '', genre: '', esrbRating: '' },
      setFilters: jest.fn()
    }));
    render(<Content />);
    expect(screen.getByText(/error/i)).toBeInTheDocument();
    expect(screen.getByText(/network error/i)).toBeInTheDocument();
  });

  it('shows no video games found', () => {
    useVideoGames.mockImplementation(() => ({
      videoGames: [],
      loading: false,
      error: null,
      filters: { title: '', developer: '', publisher: '', genre: '', esrbRating: '' },
      setFilters: jest.fn()
    }));
    render(<Content />);
    expect(screen.getByText(/no video games found/i)).toBeInTheDocument();
  });
});
