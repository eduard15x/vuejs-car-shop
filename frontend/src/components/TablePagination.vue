<template>
    <div class="pagination">
        <div class="pagination__info">
            <p>
                Showing
                <span> {{ startListCount }}  </span>
                -
                <span> {{ endListCount < totalCount ? endListCount : totalCount }}  </span>
                of
                <span> {{ totalCount }}  </span>
                results
            </p>
        </div>

        <div class="pagination__controls">
            <button
                :disabled="prevBtnDisabled"
                class="click prev page"
                @click.prevent="prevPage"
            >
                Prev
            </button>
            <button
                :disabled="nextBtnDisabled"
                class="click next page"
                @click.prevent="nextPage"
            >
                Next
            </button>
        </div>
    </div>
</template>

<script>
export default {
    name: 'TablePagination',
    props: {
        currentPage: {
            type: Number,
            required: true,
        },
        pageSize: {
            type: Number,
            required: true,
        },
        totalCount: {
            type: Number,
            required: true,
        },
        handlePageChange: {
            type: Function,
            required: true,
        },
    },
    data() {
        return {
            startListCount: 1,
            endListCount: this.pageSize,
            remainingItems: this.totalCount % this.pageSize,
            prevBtnDisabled: this.startListCount <= 1 && this.endListCount === this.pageSize,
            nextBtnDisabled: this.endListCount === this.totalCount,
        }
    },
    methods: {
        prevPage() {
            if (this.startListCount === 1) {
                return;
            }

            this.startListCount= this.startListCount - this.pageSize;

            if (this.endListCount === this.totalCount) {
                this.endListCount = this.endListCount - this.remainingItems;
            } else {
                this.endListCount = this.endListCount - this.pageSize;
            }

            this.handlePageChange(this.currentPage - 1);
            this.prevBtnDisabled = this.startListCount <= 1 && this.endListCount === this.pageSize;
            this.nextBtnDisabled = this.endListCount === this.totalCount;
        },
        nextPage() {
            if (this.endListCount === this.totalCount) {
                return;
            }

            this.startListCount= this.startListCount + this.pageSize;

            if (this.totalCount - (this.endListCount + this.remainingItems) === 0) {
                this.endListCount = this.totalCount;
            } else {
                this.endListCount = this.endListCount + this.pageSize;
            }

            this.handlePageChange(this.currentPage + 1);
            this.prevBtnDisabled = this.startListCount <= 1 && this.endListCount === this.pageSize;
            this.nextBtnDisabled = this.endListCount === this.totalCount;
        }
    }
}
</script>