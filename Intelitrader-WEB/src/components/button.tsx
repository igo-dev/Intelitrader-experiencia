import React, { FC } from "react"; 

type IProps = {
    setState: React.Dispatch<React.SetStateAction<boolean>>;
    setTitleModal:React.Dispatch<React.SetStateAction<string>>;
    setBtnTextModal:React.Dispatch<React.SetStateAction<string>>;
    text:string
}

export const Button:FC<IProps> = (props) => {

    const clickHandler = () => {
        props.setTitleModal('Cadastrar')
        props.setBtnTextModal('Cadastrar')
        props.setState(true)
    }

    return(
        <button onClick={clickHandler} className="btn-outline">{props.text}</button>
    )
}