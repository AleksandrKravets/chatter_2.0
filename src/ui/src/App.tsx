import React from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom';

import { Chat, Auth, Register } from './pages';
import store from './redux/store';
import { useAuth } from './utils/auth/auth-provider';

import './App.less';
import styles from './App.module.scss';

const Router = () => {
  const logged = useAuth();

  return <Provider store={store}>
    <BrowserRouter>
      <Switch>
        {!logged && <>
          <Route path="/login" component={Auth} exact />
          <Route path="/register" component={Register} exact />
          <Redirect to="/login" />
        </>}
        {logged && <>
          <Route path="/chat" component={Chat} exact />
          <Redirect to="/chat" />
        </>}
      </Switch>
    </BrowserRouter>
  </Provider>
};

const App = () => {
  return (
    <div className={styles.app}>
      <Router />
    </div >
  );
}

export default App;
