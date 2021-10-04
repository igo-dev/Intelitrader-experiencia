import React, { FC, useEffect, useState } from "react";
import { IClient } from "../interfaces/IClient";
import { UserService } from "../services/UserService";
import { Button } from "./button";
import { Dropdown } from "./dropdown";

type Props = {
    setState: React.Dispatch<React.SetStateAction<boolean>>;
    setTitleModal:React.Dispatch<React.SetStateAction<string>>;
    setBtnTextModal:React.Dispatch<React.SetStateAction<string>>;
    setId:React.Dispatch<React.SetStateAction<string>>;
};

export const TableSearch:FC<Props> = ( props ) => {

    const [searchInput, setSearchInput] = useState<string>('');
    const tableHeader:string[] = ['Nome', 'Idade', 'Sexo'];
    const [tableData, setTableData] = useState<IClient[]>([{id: 'Loading', name:'Loading', age:'Loading',sex:'Loading'}]);

    useEffect(() => {
        if (searchInput.length > 0) {
            UserService.searchUsers(searchInput)
            .then((searchResult) => setTableData(searchResult))
        } else{
            UserService.getUsers()
            .then((users) => setTableData(users))
        }
    },[searchInput])

    return(
        <div className="table-search">
            <div className="input-container">
                <input 
                    id="search-users"
                    onChange={event => (setSearchInput(event.target.value))} 
                    type='search' 
                    value={searchInput}
                    className="input"/>
                    <Button setState={props.setState} 
                            setBtnTextModal={props.setBtnTextModal} 
                            setTitleModal={props.setTitleModal} 
                            text={"Novo"}/>
            </div>
            <div className="table-result-count">MOSTRANDO {tableData.length} DE {tableData.length} RESULTADO(S)</div>
            <div className="table-container">
                
                <ol className="table-header">
                {
                    tableHeader.map((collumn) => { return (
                        <li key={collumn}>{collumn}</li>
                    )})
                }
                    <li></li>
                </ol>
                    {
                        tableData.map((client) => { return (
                            <ol key={client.id} className="table-line">
                                <li>{client.name}</li>
                                <li>{client.age}</li>
                                <li>{client.sex}</li>
                                <li className="table-line-btn"><Dropdown setBtnTextModal={props.setBtnTextModal} setTitleModal={props.setTitleModal} setState={props.setState} id={client.id} setId={props.setId} /><img src="./assets/dots-icon.svg" alt="" /></li>
                            </ol>
                        )})
                    }
            </div>
            </div>
    )
   
}
