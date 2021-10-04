import React, { FC } from 'react';
import { Header } from './components/header';
import { Footer } from './components/footer';

import './index.css'
import { Home } from './routes/Home';

const App:FC = () => {

  return (
    <div className="App">
      <Header/>
        <main>
          <Home/>
        </main>
        <Footer/>
    </div>
  );
}

export default App;
