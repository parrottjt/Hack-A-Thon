import React from 'react'

const VideoGameCard = ({ data }) => {
  return (
    <div>
      <h2>{data.title}</h2>
      <p>Description: {data.description}</p>
      <p>Developer: {data.developer}</p>
      <p>Publisher: {data.publisher}</p>
      <p>Release Date: {data.releaseDate}</p>
      <p>Genre: {data.genre}</p>
      <p>Rating: {data.rating}</p>
    </div>
  )
}

export default VideoGameCard