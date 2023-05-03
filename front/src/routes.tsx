import React from 'react';
import { Route, Routes } from 'react-router-dom'

import Home from './pages/home'

const PageRoutes = () => {
    return (
        <Routes>
            <Route path="/home" element={<Home />}/>
            <Route path="/pets" element={<h1>pets</h1>}/>
            <Route path="/authenticate" element={<h1>authenticate</h1>}/>
            <Route path='*' element={<Home />}/>
        </Routes>
    )
}

export default PageRoutes;