import '@testing-library/jest-dom';

import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import VideoGameCard from './VideoGameCard';

describe('VideoGameCard', () => {
  const mockData = {
    id: 1,
    title: 'Halo',
    developer: 'Bungie',
    publisher: 'Microsoft',
    genre: 'Shooter',
    esrbRating: 3,
    gameCoverImageSrc: 'cover.jpg',
    description: 'A sci-fi shooter.',
    releaseDate: '2001-11-15'
  };

  it('renders game info and image', () => {
    render(<VideoGameCard data={mockData} />);
    expect(screen.getByText('Halo')).toBeInTheDocument();
    expect(screen.getByText(/Shooter/)).toBeInTheDocument();
    expect(screen.getByText(/Mature/)).toBeInTheDocument();
    expect(screen.getByAltText(/game cover/i)).toBeInTheDocument();
  });

  it('shows modal with details on card click and closes on overlay click', () => {
    render(<VideoGameCard data={mockData} />);
    fireEvent.click(screen.getByRole('img'));
    // The modal title is a <h2> with text 'Halo', which is the second occurrence
    const modalTitle = screen.getAllByText('Halo')[1];
    expect(modalTitle.tagName).toBe('H2');
    expect(screen.getByText(/A sci-fi shooter/)).toBeInTheDocument();
    expect(screen.getByText(/Bungie/)).toBeInTheDocument();
    expect(screen.getByText(/Microsoft/)).toBeInTheDocument();
    expect(screen.getByText(/2001-11-15/)).toBeInTheDocument();
    // Close modal
    fireEvent.click(screen.getByText(/A sci-fi shooter/).closest('.modal-overlay'));
    expect(screen.queryByText(/A sci-fi shooter/)).not.toBeInTheDocument();
  });
});
