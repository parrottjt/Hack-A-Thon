
import React from 'react';
import { useVideoGames } from './features/useVideoGame/useVideoGames';
import VideoGameCard from './VideoGameCard';
import "./Content.css";

const Content = () => {
    const { videoGames, loading, error, filters, setFilters } = useVideoGames({
        title: '',
        developer: '',
        publisher: '',
        genre: '',
        esrbRating: ''
    });

    const handleChange = (e) => {
        setFilters(f => ({ ...f, [e.target.name]: e.target.value }));
    };

    return (
        <main className="content">
            <div className="filter-container">
                <input
                    type="text"
                    name="title"
                    placeholder='Search by title...'
                    value={filters.title}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="developer"
                    placeholder='Search by developer...'
                    value={filters.developer}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="publisher"
                    placeholder='Search by publisher...'
                    value={filters.publisher}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="genre"
                    placeholder='Search by genre...'
                    value={filters.genre}
                    onChange={handleChange}
                />
                <select
                    name="esrbRating"
                    placeholder='Select ESRB Rating...'
                    value={filters.esrbRating}
                    onChange={handleChange}
                >
                    <option value="">All Ratings</option>
                    <option value="E">E (Everyone)</option>
                    <option value="E10">E10+ (Everyone 10+)</option>
                    <option value="T">T (Teen)</option>
                    <option value="M">M (Mature)</option>
                    <option value="AO">AO (Adults Only)</option>
                </select>
            </div>
            {loading ? (
                <p>
                    <em>Loading... Please wait.</em>
                </p>
            ) : error ? (
                <p><em>Error: {error.message}</em></p>
            ) : videoGames.length === 0 ? (
                <p><em>No video games found.</em></p>
            ) : (
                <div className="card-container">
                    {videoGames.map(game => (
                        <VideoGameCard key={game.id} data={game} />
                    ))}
                </div>
            )}
        </main>
    );
}

export default Content;