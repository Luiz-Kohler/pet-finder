import React from 'react';
import './index.css'
import Imagem from './../../assets/home-page.png'
import Button from './../../components/button'

const Home: React.FC = () => {
    return (
        <div className='container'>
            <div className='center'>
                <div className='tittle'>
                    <h1 className='blue'>Pet</h1>
                    <div className='finder-box'>
                        <h1>Finder</h1>
                        <h1 className='blue'>.</h1>
                    </div>
                </div>

                <div className="content">
                    <div className="description">
                        <span>
                            Welcome to our pet adoption website! Here you will find a variety of pets available for adoption,
                            so that you can give them a loving home.
                        </span>
                        <span>
                            Here on our website, we believe that all pets deserve a home and a family that will love and care for them.
                            That's why we offer a range of animal options for adoption, from dogs and cats to birds.
                        </span>
                        <span>
                            We have detailed information on each pet available for adoption, including photos and personality descriptions,
                            to help you find your perfect companion. Additionally,
                            our team of animal experts can help you with any questions or concerns you may have about pet adoption.
                        </span>
                        <span>
                            Don't waste any more time,
                            come check out our pets available for adoption and find your new best friend today!
                        </span>

                        <div className="actions">
                            <Button text='See more pets'></Button>
                            <Button text='Create account'></Button>
                        </div>
                    </div>
                    <div className="image">
                        <img src={Imagem} />
                        <div className='reasons'>
                            <div className='finder-box'>
                                <h2>Reasons to adopt</h2>
                            </div>
                            <span>
                                Adopting a pet can be one of the best decisions that anyone can make.
                                Not only will you be giving an animal a new chance at life,
                                but you will also benefit in various ways.
                            </span>
                            <span>
                                Pets, just like dogs and cats,
                                are known to be loving and loyal animals that never fail to lift our spirits no matter how tough the day.
                                They are an endless source of love and companionship, which can significantly improve our mental health and well-being.
                            </span>
                            <span>
                                Moreover, adopting a pet can help you to be more responsible and committed.
                                Taking care of a pet requires time, energy, and dedication,
                                which can teach you to be more organized and responsible in other areas of your life.
                            </span>
                            <span>
                                Another advantage of adopting a pet is that you will be doing a good deed.
                                Many animals are abandoned and left in shelters waiting for a new home,
                                and adopting a pet means that you will be giving them a loving and safe home.
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
};

export default Home;