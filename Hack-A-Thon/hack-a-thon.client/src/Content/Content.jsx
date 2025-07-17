import { useEffect, useState } from 'react';
import VideoGameCard from './VideoGameCard';
import "./Content.css";

const Content = () => {
    const [videoGames, setVideoGames] = useState([]);
    const [loading, setLoading] = useState(true);
    const [dto, setDto] = useState({
        title: '',
        developer: '',
        publisher: '',
        genre: '',
        esrbRating: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setDto(prev => ({ ...prev, [name]: value }));
    };

    useEffect(() => {
        populateVideoGames();
    }, []);

    async function populateVideoGames() {
        try {
            const response = await fetch('https://localhost:7093/VideoGames');
            if (!response.ok) {
                throw new Error('Failed to fetch video games');
            }

            const data = await response.json();
            setVideoGames(data);
        } catch (error) {
            console.error("Error fetching video games:", error);
            setVideoGames([]); // fallback to empty
        } finally {
            setLoading(false);
        }
    }

    async function filterVideoGames() {
        try {
            setLoading(true);
            const queryString = new URLSearchParams(dto).toString();
            const response = await fetch('https://localhost:7093/VideoGame?' + queryString);
            if (!response.ok) {
                throw new Error('Failed to fetch video games');
            }

            const data = await response.json();
            setVideoGames(data);
        } catch (error) {
            console.error("Error fetching video games:", error);
            setVideoGames([]); // fallback to empty
        } finally {
            setLoading(false);
        }
    }

    return (
        <main className="content">
            <div className="filter-container">
                <input
                    type="text"
                    name="title"
                    placeholder='Search by title...'
                    value={dto.title}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="developer"
                    placeholder='Search by developer...'
                    value={dto.developer}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="publisher"
                    placeholder='Search by publisher...'
                    value={dto.publisher}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="genre"
                    placeholder='Search by genre...'
                    value={dto.genre}
                    onChange={handleChange}
                />
                <select
                    name="esrbRating"
                    placeholder='Select ESRB Rating...'
                    value={dto.esrbRating}
                    onChange={handleChange}
                >
                    <option value="">All Ratings</option>
                    <option value="E">E (Everyone)</option>
                    <option value="E10">E10+ (Everyone 10+)</option>
                    <option value="T">T (Teen)</option>
                    <option value="M">M (Mature)</option>
                    <option value="AO">AO (Adults Only)</option>
                </select>
                <button onClick={filterVideoGames}>Filter</button>
            </div>
            {loading ? (
                <p>
                    <em>Loading... Please refresh once the ASP.NET backend has started. See{' '}
                        <a href="https://aka.ms/jspsintegrationreact" target="_blank" rel="noopener noreferrer">
                            this guide
                        </a> for more details.
                    </em>
                </p>
            ) :
                (

                    videoGames.length === 0 ? (
                        <p><em>No video games found.</em></p>
                    ) :
                        (
                            <div className="card-container">
                                {videoGames.map(game => (
                                    <VideoGameCard key={game.id} data={game} />
                                ))}
                            </div>
                        )
                )}
        </main>
    );
}


export default Content;