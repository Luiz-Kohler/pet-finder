import React from 'react';
import './index.css'
import TextField from '@mui/material/TextField';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox'
import Button from '@mui/material/Button'
import Imagem from './../../assets/Dog-Wallpaper-For-Desktop.jpg'

const Authenticate: React.FC = () => {
    return (
        <div className='authenticate-container' style={{ backgroundImage: `url(${Imagem})` }} >
            <div className='form'>
                <FormGroup className="login-box">
                    <div className='tittle'>
                        <h1 className='blue'>Pet</h1>
                        <div className='finder-box'>
                            <h1>Finder</h1>
                            <h1 className='blue'>.</h1>
                        </div>
                    </div>
                    <TextField className='field' label="Email" />
                    <div className='field-checkbox'>
                        <TextField className='field' type="password" label="Password" />
                        <FormControlLabel control={<Checkbox defaultChecked />} label="Remeber me" />
                    </div>
                    <Button variant="contained">Login</Button>
                    <Button variant="text">Register Now</Button>
                </FormGroup>
            </div>
        </div>
    )
};

export default Authenticate;