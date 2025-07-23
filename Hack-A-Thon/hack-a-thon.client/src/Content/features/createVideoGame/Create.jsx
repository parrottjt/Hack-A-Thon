import React, { useState } from 'react'

const Create = () => {
    const [showModal, setShowModal] = React.useState(false);

    const [dto, setDto] = useState({
        title: '',
        imageUrl: '',
        description: '',
        developer: '',
        publisher: '',
        genre: '',
        esrbRating: 'Undefined'
    });

    const handleChange = (e) => {
        setDto(prev => ({ ...prev, [e.target.name]: e.target.value }));
    }

    return (
        <div>
            <button className='createButton' onClick={() => setShowModal(true)}>
                Create
            </button>
            {showModal &&
                (
                    <div className="modal-overlay" onClick={() => setShowModal(false)}>
                        <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                            <div className="create-container">
                                <input
                                    type="text"
                                    name="title"
                                    placeholder='Add title...'
                                    value={dto.title}
                                    onChange={handleChange}
                                />
                                <input
                                    type="text"
                                    name="imageUrl"
                                    placeholder='Add image URL...'
                                    value={dto.imageUrl}
                                    onChange={handleChange}
                                />
                                <input
                                    type="text"
                                    name="description"
                                    placeholder='Add description...'
                                    value={dto.description}
                                    onChange={handleChange}
                                />
                                <input
                                    type="text"
                                    name="developer"
                                    placeholder='Add developer...'
                                    value={dto.developer}
                                    onChange={handleChange}
                                />
                                <input
                                    type="text"
                                    name="publisher"
                                    placeholder='Add publisher...'
                                    value={dto.publisher}
                                    onChange={handleChange}
                                />
                                <input
                                    type="text"
                                    name="genre"
                                    placeholder='Add genre...'
                                    value={dto.genre}
                                    onChange={handleChange}
                                />
                                <select
                                    name="esrbRating"
                                    placeholder='Select ESRB Rating...'
                                    value={dto.esrbRating}
                                    onChange={handleChange}
                                >
                                    <option value="Undefined">No Rating</option>
                                    <option value="E">E (Everyone)</option>
                                    <option value="E10">E10+ (Everyone 10+)</option>
                                    <option value="T">T (Teen)</option>
                                    <option value="M">M (Mature)</option>
                                    <option value="AO">AO (Adults Only)</option>
                                </select>
                                <button onClick={handleCreate}>Create</button>
                                <button onClick={() => setShowModal(false)}>Close</button>
                            </div>
                        </div>

                    </div>
                )
            }
        </div>
    )
}

export default Create