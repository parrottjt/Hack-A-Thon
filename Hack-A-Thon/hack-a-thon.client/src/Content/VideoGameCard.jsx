import React from 'react';
import './VideoGameCard.css';

const VideoGameCard = ({ data }) => {
    const ESRBRating = {
        0: "Everyone",
        1: "Everyone 10+",
        2: "Teen",
        3: "Mature",
        4: "Adults Only"
    }
    const [showModal, setShowModal] = React.useState(false);

    return (
        <div>
            <div className="card" onClick={() => setShowModal(true)}>
                <img src={data.gameCoverImageSrc} alt={"Game Cover"} className="card-image" />
                <div className="card-content">
                    <p><strong>{data.title}</strong></p>
                    <p>Genre: {data.genre}</p>
                    <p>Rating: {ESRBRating[data.esrbRating]}</p>
                </div>
            </div>
            {showModal &&
                (
                    <div className="modal-overlay" onClick={() => setShowModal(false)}>
                        <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                            <h2>{data.title}</h2>
                            <p>{data.description || 'No description provided.'}</p>
                            <p><strong>Developer:</strong> {data.developer}</p>
                            <p><strong>Publisher:</strong> {data.publisher}</p>
                            <p><strong>Release Date:</strong> {data.releaseDate}</p>
                            <p><strong>Genre:</strong> {data.genre}</p>
                            <p><strong>Rating:</strong> {ESRBRating[data.esrbRating]}</p>
                        </div>
                    </div>
                )}
        </div>
    );
}

export default VideoGameCard