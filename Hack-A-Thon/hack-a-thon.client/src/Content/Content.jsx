import { useEffect, useState } from 'react';
import VideoGameCard from './VideoGameCard';

const Content = () => {
    const [videoGames, setVideoGames] = useState([]);
    const [loading, setLoading] = useState(true);

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
        return (
            <div>
            {loading ? (
                <p>
                    <em>Loading... Please refresh once the ASP.NET backend has started. See{' '}
                        <a href="https://aka.ms/jspsintegrationreact" target="_blank" rel="noopener noreferrer">
                            this guide
                        </a> for more details.
                    </em>
                </p>
            ) : (
                videoGames.length === 0 ? (
                    <p><em>No video games found.</em></p>
                ) : (
                    videoGames.map(game => (
                        <VideoGameCard key={game.id} data={game} />
                    ))
                )
            )}
        </div>
    );
}


export default Content;