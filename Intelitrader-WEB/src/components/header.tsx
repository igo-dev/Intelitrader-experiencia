import React, { FC } from 'react'

export const Header:FC = () => {
    return (
    <header className="header">
        <img className="header-logo" src="./assets/logo.svg" alt="" />
        <img className="header-hero" src="./assets/header-hero.svg" alt="" />
    </header>
  )
}