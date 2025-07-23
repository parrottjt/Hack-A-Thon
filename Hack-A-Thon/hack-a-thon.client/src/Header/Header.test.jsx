import '@testing-library/jest-dom';

import React from 'react';
import { render, screen } from '@testing-library/react';
import Header from './Header';

describe('Header', () => {
  it('renders the main heading and subtitle', () => {
    render(<Header />);
    expect(screen.getByRole('heading', { name: /video game library/i })).toBeInTheDocument();
    expect(screen.getByText(/explore your favorite video games/i)).toBeInTheDocument();
  });
});
