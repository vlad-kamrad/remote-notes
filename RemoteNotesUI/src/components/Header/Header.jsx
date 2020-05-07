import React, { useContext, useEffect } from 'react';
import { Form, Navbar, Nav, DropdownButton, Dropdown } from 'react-bootstrap';
import Auth from '../../utils/Auth';
import AuthStore from '../../utils/AuthStore';
import NotesContext from '../../contexts/NotesContext/NotesContext'; // TODO: Use index.js

import './Header.css';

const Header = () => {
  const [context, setContext] = useContext(NotesContext);

  useEffect(() => {
    const { username, roles } = AuthStore.getAuth();
    setContext(p => ({
      ...p,
      currentUser: {
        username,
        userRoles: roles,
        isAdmin: roles.some(x => x === 'Admin')
      }
    }));
  }, []);

  return (
    <Navbar bg='dark' variant='dark'>
      <Navbar.Brand href='/'>Remote Notes</Navbar.Brand>
      <Nav className='mr-auto'>
        <Nav.Link href='/'>Home</Nav.Link>
        {context.currentUser.isAdmin && (
          <Nav.Link href='/admin-panel'>Admin Panel</Nav.Link>
        )}
        <Nav.Link href='/about'>About</Nav.Link>
      </Nav>
      <Form inline>
        <DropdownButton
          id='dropdown-basic-button'
          variant='outline-info'
          title='Accout'
          alignRight
        >
          <Dropdown.Item href='/account-settings'>Settings</Dropdown.Item>
          <Dropdown.Item onClick={Auth.signOut}>Logout</Dropdown.Item>
        </DropdownButton>
      </Form>
    </Navbar>
  );
};

export default Header;
