import React from 'react';
import classNames from 'classnames';
import { Dropdown, Menu } from 'antd';
import { useDispatch } from 'react-redux';

import Body from '../body';
import Time from '../time';
import { actions as inputActions, InputMode } from './../../../redux/input-reducer';
import { fetchDeleteMessage } from '../../../redux/messages-reducer';

import styles from './content.module.scss';

type Props = {
    id: number
    creationTime: string
    text: string
    messageToReplyOwnerNickname: string | null | undefined
    messageToReplyText: string | null | undefined
    isCurrentUserMessage: boolean
    isReply: boolean
    isReplyToCurrentUser: boolean
}

const Content: React.FC<Props> = ({
    id,
    creationTime,
    text,
    messageToReplyOwnerNickname,
    messageToReplyText,
    isCurrentUserMessage,
    isReply,
    isReplyToCurrentUser,
}) => {
    const dispatch = useDispatch();

    const onEdit = () => {
        dispatch(inputActions.setSelectedMessage(id, InputMode.Edit));
        dispatch(inputActions.setInputText(text));
    }

    const onDelete = () => dispatch(fetchDeleteMessage(id))

    const onReply = () => dispatch(inputActions.setSelectedMessage(id, InputMode.Reply));

    return (
        <Dropdown
            overlay={isCurrentUserMessage
                ? <Menu>
                    <Menu.Item onClick={onEdit} key="1">Edit</Menu.Item>
                    <Menu.Item onClick={onDelete} key="2" style={{ color: 'red' }}>Delete</Menu.Item>
                </Menu>
                : <Menu>
                    <Menu.Item onClick={onReply} key="1">Reply</Menu.Item>
                </Menu>
            }
            trigger={['contextMenu']}
        >
            <div className={classNames(styles.content,
                {
                    [styles['content_background-grey']]: !isCurrentUserMessage && !isReplyToCurrentUser,
                    [styles['content_background-green']]: isCurrentUserMessage && !isReplyToCurrentUser,
                    [styles['content_background-yellow']]: isReplyToCurrentUser
                }
            )}>
                <Body
                    text={text}
                    messageToReplyOwnerNickname={messageToReplyOwnerNickname}
                    messageToReplyText={messageToReplyText}
                    isReply={isReply}
                    isCurrentUserMessage={isCurrentUserMessage}
                    isReplyToCurrentUser={isReplyToCurrentUser}
                />
                <Time creationTime={creationTime} />
            </div>
        </Dropdown>
    )
}

export default Content;