import React, { useState } from "react"
import { Button } from "../components/button"
import { Dropdown } from "../components/dropdown"
import { Modal } from "../components/modal"
import { TableSearch } from "../components/tableSearch"

export const Home = () => {

    const [openModal, setOpenModal] = useState<boolean>(false)
    const [btn, setBtn] = useState<string>('Default')
    const [title, setTitle] = useState<string>('Default')
    const [id, setId] = useState<string>('')

    return (
        <div>
            {openModal && <Modal id={id} setTitleModal={title} setBtnTextModal={btn} setState={setOpenModal}/>}
            <label className="big-label" htmlFor="search-users">Clientes</label>
            <TableSearch setId={setId} setBtnTextModal={setBtn} setTitleModal={setTitle} setState={setOpenModal}/>
        </div>
    )
}