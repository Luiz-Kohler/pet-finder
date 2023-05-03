import React from 'react';
import './index.css';

interface ButtonProps {
    text: string;
    onClick?: () => void;
    disabled?: boolean;
}

const Button: React.FC<ButtonProps> = ({ text, onClick, disabled }) => {
    return (
        <button className="Button" onClick={onClick} disabled={disabled}>
            {text}
        </button>
    );
};

export default Button;
