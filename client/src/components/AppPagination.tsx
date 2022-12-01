import { Box, Typography, Pagination } from '@mui/material';
import { useState } from 'react';
import { MetaData } from '../models/pagination';

interface Props {
    metaData: MetaData;
    onPageChange: (page: number) => void;
}

export default function AppPagination({ metaData, onPageChange }: Props) {
    const { currentPage, totalCount, totalPage, pageSize } = metaData;
    const [pageNumber, setPageNumber] = useState(currentPage);

    function handlePageChange(page: number) {
        setPageNumber(page);
        onPageChange(page);
    }

    return (
        <Box display='flex' justifyContent='space-between' align-items='center'>
            <Typography>
                Displaying {(currentPage - 1) * pageSize + 1}-
                {currentPage * pageSize > totalCount
                    ? totalCount
                    : currentPage * pageSize}{' '}
                of {totalCount} items
            </Typography>
            <Pagination
                color='secondary'
                size='large'
                count={totalPage}
                page={pageNumber}
                onChange={(e, page) => handlePageChange(page)}
            />
        </Box>
    );
}