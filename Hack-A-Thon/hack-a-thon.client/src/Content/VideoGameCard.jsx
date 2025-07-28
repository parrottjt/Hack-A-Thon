import React, { useState } from 'react';
import './VideoGameCard.css';


const VideoGameCard = ({ data, onUpdate, onDelete }) => {
    const ESRBRating = {
        0: "Everyone",
        1: "Everyone 10+",
        2: "Teen",
        3: "Mature",
        4: "Adults Only",
        "E": "Everyone",
        "E10": "Everyone 10+",
        "T": "Teen",
        "M": "Mature",
        "AO": "Adults Only"
    };
    const [showModal, setShowModal] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [form, setForm] = useState({
        gameCoverImageSrc: data.gameCoverImageSrc,
        title: data.title,
        genre: data.genre,
        developer: data.developer,
        publisher: data.publisher,
        esrbRating: data.esrbRating,
        description: data.description,
        releaseDate: data.releaseDate,
        // add other fields as needed
    });

    const handleChange = e => {
        const { name, value } = e.target;
        setForm(f => ({ ...f, [name]: value }));
    };

    const handleSave = async () => {
        // Always use the current data.id, not from form
        const formWithId = { ...form, Id: data.id };
        await onUpdate(data.id, formWithId);
        setIsEditing(false);
        setShowModal(false);
    };

    const handleDelete = async () => {
        if (window.confirm('Are you sure you want to delete this game?')) {
            await onDelete(data.id);
            setShowModal(false);
        }
    };

    return (
        <div>
            <div className="card" onClick={() => setShowModal(true)}>
                {data.gameCoverImageSrc ? (
                    <img src={data.gameCoverImageSrc} alt="Game Cover" className="card-image" />
                ) : (
                    <div className="card-image placeholder">No Image</div>
                )}
                <div className="card-content">
                    <p><strong>{data.title}</strong></p>
                    <p>Genre: {data.genre}</p>
                    <p>Rating: {ESRBRating[data.esrbRating] || data.esrbRating || "Unknown"}</p>
                </div>
            </div>
            {showModal && (
                <div className="modal-overlay" onClick={() => setShowModal(false)}>
                    <div className="modal-content" onClick={e => e.stopPropagation()}>
                        {isEditing ? (
                            <>
                                <input name="title" value={form.title} onChange={handleChange} />
                                <input name="genre" value={form.genre} onChange={handleChange} />
                                <input name="developer" value={form.developer} onChange={handleChange} />
                                <input name="publisher" value={form.publisher} onChange={handleChange} />
                                <select name="esrbRating" value={form.esrbRating} onChange={handleChange}>
                                    <option value={0}>Everyone</option>
                                    <option value={1}>Everyone 10+</option>
                                    <option value={2}>Teen</option>
                                    <option value={3}>Mature</option>
                                    <option value={4}>Adults Only</option>
                                </select>
                                <textarea name="description" value={form.description} onChange={handleChange} />
                                <input name="releaseDate" value={form.releaseDate} onChange={handleChange} />
                                <button onClick={handleSave}>Save</button>
                                <button onClick={() => setIsEditing(false)}>Cancel</button>
                            </>
                        ) : (
                            <>
                                <h2>{data.title}</h2>
                                <p>{data.description || 'No description provided.'}</p>
                                <p><strong>Developer:</strong> {data.developer}</p>
                                <p><strong>Publisher:</strong> {data.publisher}</p>
                                <p><strong>Release Date:</strong> {data.releaseDate}</p>
                                <p><strong>Genre:</strong> {data.genre}</p>
                                <p><strong>Rating:</strong> {ESRBRating[data.esrbRating]}</p>
                                <button onClick={() => setIsEditing(true)}>Edit</button>
                                <button onClick={handleDelete}>Delete</button>
                            </>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};

export default VideoGameCard