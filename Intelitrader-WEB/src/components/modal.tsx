import React, { Dispatch, FC, FormEvent, FormEventHandler, SetStateAction, useState } from 'react'
import { IPatchOp } from '../interfaces/IPatchOp';
import { UserService } from '../services/UserService'

type Props = {
    setState: React.Dispatch<React.SetStateAction<boolean>>
    setTitleModal:string
    setBtnTextModal:string
    id:string
};


export const Modal:FC<Props> = ( props ) => {

    const [name, setName] = useState<string>('')
    const [dateBith, setDateBirth] = useState<string>('')
    const [sex, setSex] = useState<string>('')

    const createHandler = () => {
        UserService.createUser({name:name, dateBirth:dateBith, sex:sex})
        .then((r) => {if(r == 201) {window.location.reload()}});
    }

    const editHandler = () => {
        let jsonPatch:IPatchOp[] = [];
        if (name.length > 0) {
            jsonPatch.push({path:'/name', op:'replace', value:name})
        }
        if (dateBith.length > 0) {
            jsonPatch.push({path:'/birthDate', op:'replace', value:dateBith})
        }
        if (sex.length > 0) {
            jsonPatch.push({path:'/sex', op:'replace', value:sex})
        }

        UserService.editUser(props.id, jsonPatch)
        .then((r) => {if(r == 200) {window.location.reload()}});
    }

    return(
        <div className="openModal">
            <form>
                <div className='modal-header'>
                    <p>{props.setTitleModal}</p>
                    <p className='btn' onClick={() => {props.setState(false)}}> Cancelar </p>
                </div>
                <label htmlFor="name">Nome*</label>
                <input id ="name" className='input' onChange={(event) => (setName(event.target.value))} type='text' value={name} />
                <label htmlFor="dateBirth">Data de Nascimento*</label>
                <input id ="dateBirth" className='input' onChange={(event) => (setDateBirth(event.target.value))} type='date' value={dateBith}/>
                <label htmlFor="sex">Sexo*</label>
                <select id="sex" className="input" onChange={(event) => (setSex(event.target.value))}>
                    <option id="3" value='Fem'>Feminino</option>
                    <option id="2" value='Masc'>Masculino</option>
                    <option id="1" value='Outro'>Outro</option>
                </select>
                
            <button 
                onClick={props.setBtnTextModal == 'Cadastrar'?createHandler:editHandler} 
                type="button" 
            >{props.setBtnTextModal}</button>
            </form>
            
        </div>
    )
}