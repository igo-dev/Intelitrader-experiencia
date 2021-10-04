import React, { FC } from "react";
import { UserService } from "../services/UserService";

type Props = {
    id:string
    setState: React.Dispatch<React.SetStateAction<boolean>>;
    setTitleModal:React.Dispatch<React.SetStateAction<string>>;
    setBtnTextModal:React.Dispatch<React.SetStateAction<string>>;
    setId:React.Dispatch<React.SetStateAction<string>>;
};

export const Dropdown:FC<Props> = (props) => {

    const deleteHandler = () => {
        UserService.deleteUser(props.id)
        .then((r) => {if(r === 200) {window.location.reload()}});
    }

    const editHandler = () => {
        props.setTitleModal('Editar')
        props.setBtnTextModal('Atualizar')
        props.setState(true);
        props.setId(props.id)
    }

    return (
        <div className="table-dropdown">
            <div className="edit" onClick={editHandler}>Editar</div>
            <div className="delete" onClick={deleteHandler}>Deletar</div>
        </div>
    )
}