import React from 'react';
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import Notes from './components/Notes';
import AdminPanel from './components/AdminPanel';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import { initialNotesState } from '../src/constants';
import NotesProvider from './contexts/NotesContext/NotesProvider';
import 'antd/dist/antd.css';
import './App.css';

const App = () => {
  return (
    <NotesProvider initialState={initialNotesState}>
      <Router>
        <Switch>
          <Route path='/login'>
            <LoginForm />;
          </Route>
          <Route path='/registration'>
            <RegisterForm />;
          </Route>
          <Route path='/admin-panel'>
            <AdminPanel />
          </Route>
          <Route path='/'>
            <Notes />
          </Route>
        </Switch>
      </Router>
    </NotesProvider>
  );
};

export default App;
