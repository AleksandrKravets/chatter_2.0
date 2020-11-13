import { Button } from 'antd';
import React from 'react'
import { useSelector } from 'react-redux';
import { useHistory } from 'react-router-dom';

import UsersDrawer from '../drawers/online-users/online-users';
import { logout } from '../../utils/auth/auth-provider';
import { AppStateType } from '../../redux/store';

import styles from './header.module.scss';

const Header: React.FC = () => {
    const history = useHistory();

    const onlineUsersCount = useSelector((state: AppStateType) => state.users.onlineUsersCount);

    const [areUsersVisible, setUsersVisibility] = React.useState(false)

    const onUsersDrawerClose = () => setUsersVisibility(false)

    const showUsersDrawer = () => setUsersVisibility(true)

    const onLogout = () => {
        logout()
        history.push('/login')
    }

    return (
        <div className={styles.header}>
            <div className={styles.header__logo}>
                Chatter
            </div>
            <div className={styles.header__navigation}>
                <div className={styles.header__links}>
                    <Button onClick={showUsersDrawer} type="primary" shape="circle" size={'large'}>
                        <span className={styles['online-users-count']}>
                            {onlineUsersCount}
                        </span>
                    </Button>
                </div>

                <div className={styles.header__exit}>
                    <Button onClick={onLogout} type="primary" shape="circle" size={'large'}>
                        <span className={styles['logout-btn']}>
                            <i className='fas fa-door-open'></i>
                        </span>
                    </Button>
                </div>
            </div>

            <UsersDrawer isVisible={areUsersVisible} onClose={onUsersDrawerClose} />
        </div>
    )
}






export default Header;
