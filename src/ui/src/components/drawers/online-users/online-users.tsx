import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Drawer, Input } from 'antd';
import { UserOutlined } from '@ant-design/icons';

import OnlineUser from './online-user/online-user';
import { fetchOnlineUsers, OnlineUserType } from '../../../redux/users-reducer';
import { AppStateType } from '../../../redux/store';

import styles from './online-users.module.scss';

type Props = {
    isVisible: boolean
    onClose: () => void
}

const UsersDrawer: React.FC<Props> = ({ isVisible, onClose }) => {
    const [inputValue, setInputValue] = React.useState('');

    const onInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(event.target.value)
    }

    const dispatch = useDispatch();

    const onlineUsers = useSelector((state: AppStateType) => state.users.onlineUsers);
    const usersToShow = onlineUsers.filter((x: OnlineUserType) => x.nickname.toLowerCase().includes(inputValue.toLowerCase()))

    React.useEffect(() => {
        if (isVisible) {
            dispatch(fetchOnlineUsers());
        }
    }, [isVisible])

    const usersList = usersToShow && usersToShow.map(x =>
        <OnlineUser
            key={x.id}
            id={x.id}
            isMuted={x.isMuted}
            nickname={x.nickname}
            isCurrentUser={x.isCurrentUser}
        />)

    return (
        <Drawer
            title="Users"
            closable={true}
            onClose={onClose}
            visible={isVisible}
            bodyStyle={{ padding: 0 }}
            headerStyle={{borderBottom: 0}}
        >
            <div className={styles['users-drawer']}>
                <div className={styles['users-drawer__input']}>
                    <Input
                        placeholder="Enter username"
                        prefix={<UserOutlined className="site-form-item-icon" />}
                        value={inputValue}
                        onChange={onInputChange}
                    />
                </div>
                <div className={styles['users-drawer__users']}>
                    {usersList}
                </div>
            </div>
        </Drawer>
    )
}

export default UsersDrawer;